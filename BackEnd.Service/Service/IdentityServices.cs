using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
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
    private IMapper _mapper;
    public IConfiguration Configuration { get; }
    public IdentityServices(UserManager<ApplicationUser> userManager,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      BakEndContext dataContext,
      IemailService emailService,
      IMapper mapper,
      IConfiguration iConfig)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _jwtSettings = jwtSettings;
      _TokenValidationParameters = TokenValidationParameters;
      _dataContext = dataContext;
      _emailService = emailService;
      _BakEndContext = dataContext;
      _mapper = mapper;
      Configuration = iConfig;
    }
    #region LoginAsync
    public async Task<AuthenticationResult> LoginAsync(string Email,string UserName, string Password)
    {
      //var user = await _userManager.FindByEmailAsync(Email);
      //var user =  FindByEmailCustome(Email);
      var user = FindByUserNameCustom(UserName);
      if (user == null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User does not Exist" }
        };
      }

      // var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
      //var userHasValidPassword = Decrypt(user.PasswordHash,"xxx");
      var encodedPassword = EncodePasswordmosso(Password);
      if (!(encodedPassword == user.PasswordHash))
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User/Password combination wrong" }
        };
      }
      return await GenerateAutheticationForResultForUser(user);
    }
    #endregion

    #region GetPrincipalFromToken
    private ClaimsPrincipal GetPrincipalFromToken(string Token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      try
      {
        var principal = tokenHandler.ValidateToken(Token, _TokenValidationParameters, out var validtionToken);
        if (!IsJwtWithValidationSecurityAlgorithm(validtionToken))
        {
          return null;
        }
        return principal;
      }
      catch
      {
        return null;
      }
    }
    #endregion
    private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken)
    {
      return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
        jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
        StringComparison.InvariantCultureIgnoreCase);
    }

    public async Task<AuthenticationResult> RegisterAsync(string UserName, string Email, string PhoneNumber, string Password, string Roles)
    {
      //var existingUser = await _userManager.FindByEmailAsync(Email);
      var existingUser = FindByEmailCustome(Email);
      if (existingUser == null) {
        existingUser = FindByUserNameCustom(UserName);
      }
      if (existingUser != null)
      {
        return new AuthenticationResult
        {
          Errors = new[] { "User already Exist" }
        };
      }
      int num = _random.Next();
      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        PhoneNumber = PhoneNumber,
        verficationCode = num,
        //PasswordHash= Encrypt(Password,"xxx"),
        PasswordHash= EncodePasswordmosso(Password),
        userTypeId = 4,
        confirmed=false
      };

      var createdUser = await _userManager.CreateAsync(newUser);

      if (!createdUser.Succeeded)
      {
        return new AuthenticationResult
        {
          Errors = createdUser.Errors.Select(x => x.Description)
        };
      }

      //-----------------------------add Role to token------------------
      if (!string.IsNullOrEmpty(Roles))
      {
        await _userManager.AddToRoleAsync(newUser, Roles);
      }
      //-----------------------------------------------------------------

      var res = await sendVerficationToEMail(newUser.verficationCode.Value, newUser.Email);
      if (res != true)
      {
        return new AuthenticationResult
        {
          Errors = createdUser.Errors.Select(x => "email not send")
        };
      }
      else
      {
        return new AuthenticationResult
        {
          Success = true
        };
      }


    }

    private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user)
    {
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

    public async Task<Boolean> sendVerficationToEMail(int verficationCode, string Email)
    {
      return await _emailService.sendVerfication(verficationCode, Email);
    }

    public async Task<Result> verfayUser(UserVerfayRequest request)
    {
      //var user = await _userManager.FindByEmailAsync(request.Email);
      var user = FindByEmailCustome(request.Email);
      if (user.verficationCode == request.verficationCode)
      {
        user.confirmed = true;
        await _userManager.UpdateAsync(user);
        return new Result { success = true };
      }
      else
      {
        return new Result { success = false };
      }

    }

    public async Task<Result> CheckverfayUserByEmail(string username)
    {
      //var user = await _userManager.FindByEmailAsync(Email);
      var user = FindByUserNameCustom(username);
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

    public async Task<Result> updateVerficationCode(int num, string Email)
    {
     // var User = await _userManager.FindByEmailAsync(Email);
      var User = FindByEmailCustome(Email);
      User.verficationCode = num;
      await _userManager.UpdateAsync(User);
      return new Result
      {
        success = true,
        data = User
      };
    }

    public async Task<Result> getUserByEmail(string Email)
    {
      if (!string.IsNullOrEmpty(Email))
      {
        //var user = await _userManager.FindByEmailAsync(Email);
        var user = FindByEmailCustome(Email);
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

    public Result getAllRoles()
    {
      var res = _BakEndContext.Roles.ToList();
      return new Result { data = res };
    }



    public async Task<Result> updateresetPasswordCodeCode(int num, string Email)
    {
      //var User = await _userManager.FindByEmailAsync(Email);
      var User = FindByEmailCustome(Email);
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

    public async Task<Result> pagginationUser(string searchWord, int pageNumber, int pageSize)
    {
      // Get's No of Rows Count 
      int count = _dataContext.Users.Where(x =>
      ((searchWord != null ? x.Email.Contains(searchWord) : true) || (searchWord != null ? x.UserName.Contains(searchWord) : true))
      ).Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = _dataContext.Users.Where(x =>
      ((searchWord != null ? x.Email.Contains(searchWord) : true) || (searchWord != null ? x.UserName.Contains(searchWord) : true))
      ).Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();

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


    #region AddUserAsync
    public async Task<AddUserResult> AddUserAsync(string UserName, string Email, string PhoneNumber, string Password)
    {
      //var existingUser = await _userManager.FindByEmailAsync(Email);
      //var existingUser = FindByEmailCustome(Email);
      var existingUser = FindByEmailCustome(Email);
      if (existingUser == null)
      {
        existingUser = FindByUserNameCustom(UserName);
      }
      if (existingUser != null)
      {
        return new AddUserResult
        {
          Errors = new[] { "User with this email adress already Exist" }
        };
      }
      int num = _random.Next();
      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        PhoneNumber = PhoneNumber,
        verficationCode = num,
        //PasswordHash = Encrypt(Password, "xxx"),
        PasswordHash = EncodePasswordmosso(Password),
        userTypeId = 4,
        confirmed=false
      };

      //var createdUser = await _userManager.CreateAsync(newUser, Password);
      var createdUser = await _userManager.CreateAsync(newUser);

      if (!createdUser.Succeeded)
      {
        return new AddUserResult
        {
          Errors = createdUser.Errors.Select(x => x.Description)
        };
      }

      var res = await sendVerficationToEMail(newUser.verficationCode.Value, newUser.Email);
      if (res != true)
      {
        return new AddUserResult
        {
          Errors = createdUser.Errors.Select(x => "email not send")
        };
      }
      else
      {
        return new AddUserResult
        {
          Success = true,
          UserId = newUser.Id
        };
      }
    }

    #endregion

    #region DeleteUser
    public async Task<Result> DeleteUser(string UserId)
    {
      try
      {
        var deletedUser = await _userManager.FindByIdAsync(UserId);
        await _userManager.DeleteAsync(deletedUser);
        return new Result
        {
          success = true,
          code = "200"
        };
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = true,
          code = "403",
          message = "User Deleted Faild"
        };
      }
    }
    #endregion

    #region UpdateUser
    public async Task<UpdateUserResult> UpdateUser(string userId, string UserName, string Email, string PhoneNumber, string Password)
    {
      UpdateUserResult updateUserResult = new UpdateUserResult();
      var user = await _userManager.FindByIdAsync(userId);
      if (user != null)
      {
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        if (!string.IsNullOrEmpty(UserName))
        { user.UserName = UserName; }
        else
        {
          updateUserResult.Errors.Add("User Name cannot be empty");
          return updateUserResult;
        }

        if (!string.IsNullOrEmpty(Password))
        {
          //user.PasswordHash = passwordHasher.HashPassword(user, Password);
          user.PasswordHash  = EncodePasswordmosso(Password);
        }
        else {
          updateUserResult.Errors.Add("Password cannot be empty");
          return updateUserResult;
        }
        int num = _random.Next();
        user.verficationCode = num;
        user.confirmed = false;
        IdentityResult result = await _userManager.UpdateAsync(user);

        var res = await sendVerficationToEMail(user.verficationCode.Value, user.Email);
        if (res != true)
        {
          updateUserResult.Errors.Add("email not send");
        }

        if (result.Succeeded)
        {
          return new UpdateUserResult
          {
            Success = true,
            Errors = null,
            UserId = user.Id
          };
        }
        else {
          return new UpdateUserResult
          {
            Success = false,
            Errors = new List<string>() { "internal server Error" },
            UserId = null
          };
        }
      }
      else {
        return new UpdateUserResult {
          Success = false,
          Errors = new List<string>() {"user is not Exist"},
          UserId=null
        };
      }
   
    }
    #endregion

    #region getUserAndUserUserTypeByUserId
    public async Task<Result> getUserAndUserUserTypeByUserId(string UserId)
    {
      
      if (!string.IsNullOrEmpty(UserId))
      {
        var user = await _userManager.FindByIdAsync(UserId);
        List<AspNetUsersTypesViewModel> aspNetUsersTypesList = new List<AspNetUsersTypesViewModel>();
        foreach (var item in user.AspNetusertypjoin){
          var aspNetUsersTypesViewModel = new AspNetUsersTypesViewModel();
          aspNetUsersTypesViewModel.UsrTypID = item.AspNetUsersTypes.UsrTypID;
          aspNetUsersTypesViewModel.UsrTypNm = item.AspNetUsersTypes.UsrTypNm;
          aspNetUsersTypesList.Add(aspNetUsersTypesViewModel);
        }
        UserViewModel userViewModel = new UserViewModel {
         Id=user.Id, 
         UserName=user.UserName,
         Email =user.Email,
         PhoneNumber=user.PhoneNumber,
        aspNetUsersTypesViewModel = aspNetUsersTypesList
        };
        
        if (user != null)
        {
          return new Result
          {
            success = true,
            data = userViewModel
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
    #endregion
    #region HashPassowrd
    //public static string HashPassowrd(string password)
    //{
    //  string mySalt = DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt();
    //  return DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, mySalt);
    //}
    //public static bool VerifyPassowrd(string password, string hashedPassowrd)
    //{
    //  return DevOne.Security.Cryptography.BCrypt.BCryptHelper.CheckPassword(password, hashedPassowrd);
    //}
    #endregion

    private const string initVector = "tu89geji340t89u2";

    private const int keysize = 256;

    public static string Encrypt(string Text, string Key)
    {
      byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
      PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
      cryptoStream.FlushFinalBlock();
      byte[] Encrypted = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();
      return Convert.ToBase64String(Encrypted);
    }

    public static string Decrypt(string EncryptedText, string Key)
    {
      byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
      byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
      PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
      CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] plainTextBytes = new byte[DeEncryptedText.Length];
      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }


    public virtual string EncodePasswordmosso(string password)
    {
      string encodedPassword = password;
      // Dim pwdFormat As String = Config("passwordFormat")
      // If (pwdFormat = System.Web.Security.MembershipPasswordFormat.Encrypted) Then
      // encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)))
      // Else
      // If (passwordformat = System.Web.Security.MembershipPasswordFormat.Hashed) Then
      HMACSHA1 hash = new HMACSHA1();
      var passwordConfiguration = Configuration
          .GetSection("PasswordConfiguration")
          .Get<PasswordConfiguration>();
      string m_ValidationKey = passwordConfiguration.MembershipProviderValidationKey;
      if ((string.IsNullOrEmpty(m_ValidationKey) || m_ValidationKey.Contains("AutoGenerate")))
        m_ValidationKey = "FE876E90EF985641A24F77B05190FADD2EE660336C233E4707D8F08457318D6333FFF117A764D57A8" + "29E9549DCEA9883FBCD4979841CD53BC810C7538507A191";
      hash.Key = HexToByte(m_ValidationKey);
      encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
      // End If
      // End If
      return encodedPassword;
    }

    public static byte[] HexToByte(string hexString)
    {
      byte[] returnBytes = new byte[(int)((hexString.Length / (double)2) - 1 + 1)];
      int i = 0;
      while ((i < returnBytes.Length))
      {
        returnBytes[i] = Convert.ToByte(hexString.Substring((i * 2), 2), 16);
        i = (i + 1);
      }
      return returnBytes;
    }

    #region FindByEmailCustome
    public ApplicationUser FindByEmailCustome(string email) {
     return _BakEndContext.Users.FirstOrDefault(x => x.Email == email);
    }
    #endregion

    #region FindByUserNameCustom
    public ApplicationUser FindByUserNameCustom(string userName)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.UserName == userName);
    }
    #endregion

    #region GetUserByUserName
    public  Result GetUserByUserName(string userName)
    {
      var res= _BakEndContext.Users.FirstOrDefault(x => x.UserName == userName);
      return new Result {
        success = true,
        data= res
      };
    }
    #endregion
  }

}
