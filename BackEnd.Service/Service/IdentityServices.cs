using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Service.Service
{
  public class IdentityServices : IidentityServices
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationSettings _jwtSettings;
    private readonly TokenValidationParameters _TokenValidationParameters;
    private readonly BakEndContext _dataContext;
    private readonly IemailService _emailService;
    private readonly BakEndContext _BakEndContext;
    private readonly Random _random = new Random();
    public IdentityServices(UserManager<ApplicationUser> userManager,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      BakEndContext dataContext,
      IemailService emailService)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings;
      _TokenValidationParameters = TokenValidationParameters;
      _dataContext = dataContext;
      _emailService = emailService;
      _BakEndContext = dataContext;
    }

    public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
    {
      var user = await _userManager.FindByEmailAsync(Email);
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }

      var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
      if (!userHasValidPassword)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User/Password combination wrong" }
        };
      }
      return await GenerateAutheticationForResultForUser(user);
    }

  
    private ClaimsPrincipal GetPrincipalFromToken(string Token) {
      var tokenHandler = new JwtSecurityTokenHandler();
      try {
        var principal = tokenHandler.ValidateToken(Token, _TokenValidationParameters, out var validtionToken);
        if (!IsJwtWithValidationSecurityAlgorithm(validtionToken)) {
          return null;
        }
        return principal;
      }
      catch {
        return null;
      }
    }

    private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken) {
      return (validatedToken is JwtSecurityToken jwtSecurityToken)&&
        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
        StringComparison.InvariantCultureIgnoreCase);
    }

    public async Task<AuthenticationResult> RegisterAsync(string UserName,string Email,string PhoneNumber, string Password,string Roles)
    {
      var existingUser = await _userManager.FindByEmailAsync(Email);
      if (existingUser != null) {
        return new AuthenticationResult{
          Errors=new[] {"User with this email adress already Exist"}
        };
      }
      int num = _random.Next(); 
      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        PhoneNumber= PhoneNumber,
        verficationCode = num,
        userTypeId=4
      };

      var createdUser = await _userManager.CreateAsync(newUser, Password);

      if (!createdUser.Succeeded) {
        return new AuthenticationResult {
          Errors = createdUser.Errors.Select(x=>x.Description)
        };
      }

      //-----------------------------add Role to token------------------
      if (!string.IsNullOrEmpty(Roles)) { 
      await _userManager.AddToRoleAsync(newUser,Roles);
      }
      //-----------------------------------------------------------------

      var res= await sendVerficationToEMail(newUser.verficationCode.Value,newUser.Email);
      if (res != true)
      {
        return new AuthenticationResult
        {
          Errors = createdUser.Errors.Select(x => "email not send")
        };
      }
      else {
        return new AuthenticationResult
        {
          Success = true
        };
      }


    }

    private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user) {
      var TokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.JWT_Secret);
      var claims = new List<Claim> {
          new Claim(JwtRegisteredClaimNames.Sub,user.Email),
          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("id",user.Id)
          };

      //get claims of user---------------------------------------
      var Userclaims = await _userManager.GetClaimsAsync(user);
      claims.AddRange(Userclaims);
      //------------------------Add Roles to claims-----------------------------------
      var userRols = await _userManager.GetRolesAsync(user);

      foreach (var userRole in userRols)
      {
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        var role = await _roleManager.FindByNameAsync(userRole);
        if (role != null)
        {
          var roleClaims = await _roleManager.GetClaimsAsync(role);
          foreach (Claim roleClaim in roleClaims)
          {
            claims.Add(roleClaim);
          }
        }
      }
      //---------------------------------------------------------
      var TokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = TokenHandler.CreateToken(TokenDescriptor);
      await _dataContext.SaveChangesAsync();
      return new AuthenticationResult
      {
        Success = true,
        Token = TokenHandler.WriteToken(token)

      };
    }

    public async Task<Boolean> sendVerficationToEMail(int verficationCode,string Email) {
      return await _emailService.sendVerfication(verficationCode, Email);
    }

    public async Task<Result> verfayUser(UserVerfayRequest request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user.verficationCode == request.verficationCode)
      {
        user.confirmed = true;
        await _userManager.UpdateAsync(user);
        return new Result { success = true};
      }
      else {
        return new Result { success = false };
      }

    }

    public async Task<Result> CheckverfayUserByEmail(string Email)
    {
      var user = await _userManager.FindByEmailAsync(Email);
      if (user.confirmed == true)
      {
        return new Result
        {
          success = true,
          message = "user is confirmed",
        };
      }
      else
      {
        return new Result
        {
          success = false,
          message = "user is not confirmed"
        };

      }
    }

    public async Task<Result> updateVerficationCode(int num,string Email)
    {
      var User= await _userManager.FindByEmailAsync(Email);
      User.verficationCode = num;
      await _userManager.UpdateAsync(User);
      return new Result {
        success = true,
        data= User
      };
    }

    public async Task<Result> getUserByEmail(string Email)
    {
      if (!string.IsNullOrEmpty(Email)) {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user != null)
        {
          return new Result
          {
            success = true,
            data = user
          };
        }
        else {
          return new Result
          {
            success = false,
            message="user does not exist"
          };
        }
      
      }
      return new Result
      {
        success=false
      };

    }

    public Result getAllRoles()
    {
      var res= _BakEndContext.Roles.ToList();
      return new Result {data=res};
    }



    public async Task<Result> updateresetPasswordCodeCode(int num, string Email)
    {
      var User = await _userManager.FindByEmailAsync(Email);
      User.resetPasswordCode = num;
      await _userManager.UpdateAsync(User);
      return new Result
      {
        success = true,
        data = User
      };
    }

    public async Task<Result> getUserById(string UserId)
    {
      if (!string.IsNullOrEmpty(UserId))
      {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user != null)
        {
          return new Result
          {
            success = true,
            data = user
          };
        }
        else
        {
          return new Result
          {
            success = false,
            message = "user does not exist"
          };
        }

      }
      return new Result
      {
        success = false
      };

    }

    public async Task<Result> pagginationUser(int pageNumber, int pageSize)
    {
      // Get's No of Rows Count 
      int count = _dataContext.Users.Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = _dataContext.Users.Skip((CurrentPage - 1) * pageSize).Take(pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = items
      };

      // Setting Header
      // HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

      // Returing List of Customers Collections

      var res = new Result
      {
        success = true,
        data = paginationMetadata,
        code = "200",
        message = null
      };
      return res;
    }

  }
}
