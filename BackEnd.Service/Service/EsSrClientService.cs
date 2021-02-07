using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using BackEnd.DAL.Context;

namespace BackEnd.Service.Service
{
  public class EsSrClientService : IEsSrClientService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly Random _random = new Random();
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public IConfiguration Configuration { get; }
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IemailService _emailService;
    private readonly BakEndContext _BakEndContext;
    public EsSrClientService(IUnitOfWork unitOfWork, IMapper mapper,
      IConfiguration iConfig,
      RoleManager<IdentityRole> roleManager,
      IemailService emailService,
      UserManager<ApplicationUser> userManager,
      BakEndContext BakEndContext)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      Configuration = iConfig;
      _roleManager = roleManager;
      _emailService = emailService;
      _userManager = userManager;
      _BakEndContext = BakEndContext;
    }
    public async Task<Result> CreateCLient(EsSrClientViewModel esSrClientViewModel)
    {
      try
      {
        if (checEmailALreadyExist(esSrClientViewModel.Email))
        {
          return new Result
          {
            success = false,
            code = "402",
            data = null
          };
        }
        EsSrClient esSrClient = new EsSrClient();
        var obje = _mapper.Map(esSrClientViewModel, esSrClient);
        obje.IsDelete = false;
        obje.IsActive = true;
        obje.HasPassword = EncodePasswordmosso(obje.HasPassword);
        _unitOfWork.EsSrClientRepository.Insert(obje);
        var result1 = await _unitOfWork.SaveAsync();
        if (result1 == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "Resgisteration Success",
            data = null
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "Resgisteration Faild",
            data = null
          };
        }
      }
      catch (Exception ex)
      {

        return new Result
        {
          success = false,
          code = "403",
          message = "Row Added Faild",
          data = null
        };
      }
    }

    #region EncodePasswordmosso
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
    #endregion

    #region checEmailALreadyExist
    public Boolean checEmailALreadyExist(string email)
    {
      var res = _unitOfWork.EsSrClientRepository.GetEntity(filter: (x => x.Email == email));
      if (res != null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion

    #region createUserForClients
    public async Task<Result> createUserForClientsAsync()
    {
      try
      {
        List<EsSrClient> esSrClientList = _unitOfWork.EsSrClientRepository.Get().ToList();
        foreach (var item in esSrClientList)
        {
          var User = FindByEmailCustome(item.Email);
          if (User == null)
          {
            await addUser(item.Email, item.Email, item.Phone, item.HasPassword);
          }
        }
        return new Result
        {
          success = true,
          code = "200",
          message = "User add Successfuly"
        };
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "403",
          message = "faild"
        };
      }
    }
    #endregion

    #region GetCLientByEmail(string email)
    public async Task<Result> GetCLientByEmail(string email)
    {
      try
      {
        var cleint = _unitOfWork.EsSrClientRepository.GetEntity(x => x.Email == email);
        return new Result
        {
          success = true,
          code = "200",
          data = cleint,
          message = "User add Successfuly"
        };
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };
      }
    }
    #endregion

    #region createUserForTechnicalAsync
    public async Task<Result> createUserForTechnicalAsync()
    {
      try
      {
        List<EsSrTechnical> esSrClientList = _unitOfWork.EsSrTechnicalRepository.Get().ToList();
        foreach (var item in esSrClientList)
        {
          var User = FindByEmailCustome(item.Email);
          if (User == null)
          {
            await addUser(item.Email, item.Email, item.Phone, item.HashPassword);
          }
        }
        return new Result
        {
          success = true,
          code = "200",
          message = "User add Successfuly"
        };
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "403",
          message = "faild"
        };
      }
    }
    #endregion

    #region UpdateCLientFbToken(string userName,string FbToken)
    public async Task<Result> UpdateCLientFbToken(string userName, string FbToken)
    {
      try
      {
        var User = _unitOfWork.EsSrClientRepository.GetEntity(x => x.Email == userName);// FindByEmailCustome(userName);
                                                                                        //EsSrClient esSrClient = new EsSrClient();
                                                                                        //var obje = _mapper.Map(User, esSrClient);
        User.ModifiedOn = DateTime.Now;
        _unitOfWork.EsSrClientRepository.Update(User);
        var result1 = await _unitOfWork.SaveAsync();
        if (result1 == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "Update Client Faild",
            data = null
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "Update Client Faild",
            data = null
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };
      }
    }
    #endregion

    #region addUser
    public async Task addUser(string UserName, string Email, string PhoneNumber, string Password)
    {
      //int num = _random.Next(1000, 9999);
      // await _emailService.sendVerficationMobile(num, Email);

      var newUser = new ApplicationUser
      {
        Email = Email,
        UserName = UserName,
        PhoneNumber = PhoneNumber,
        //PasswordHash= Encrypt(Password,"xxx"),
        PasswordHash = Password,
        userTypeId = 4,
        confirmed = true,
        EmailConfirmed = true,
        IsApproved = true,
        PhoneNumberConfirmed = true,
        CreationDate = DateTime.Now,
        LastLoginDate = DateTime.Now,
        LastActivityDate = DateTime.Now,
        LastPasswordChangedDate = DateTime.Now,
        LastLockedOutDate = DateTime.Now,
        LastLockoutDate = DateTime.Now
      };

      await _userManager.CreateAsync(newUser);
      //-----------------------------add Role to token------------------
      await _userManager.AddToRoleAsync(newUser, "Client");
      //-----------------------------------------------------------------
    }
    #endregion

    #region FindByEmailCustome
    public ApplicationUser FindByEmailCustome(string email)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.Email == email);
    }
    #endregion

    #region CreateCLientWithSocialId
    public async Task<Result> CreateCLientWithSocialId(EsSrClientViewModel esSrClientViewModel)
    {

      try
      {
        if (!FindCustomerBySocailId(esSrClientViewModel.SocialId))
        {
          return new Result
          {
            success = false,
            code = "401",
            data = null
          };
        }


        if (!string.IsNullOrEmpty(esSrClientViewModel.Email))
        {
          if (checEmailALreadyExist(esSrClientViewModel.Email))
          {
            return new Result
            {
              success = false,
              code = "402",
              data = null
            };
          }
        }

        EsSrClient esSrClient = new EsSrClient();
        var obje = _mapper.Map(esSrClientViewModel, esSrClient);
        obje.IsDelete = false;
        obje.IsActive = true;
        obje.HasPassword = EncodePasswordmosso(obje.HasPassword);
        _unitOfWork.EsSrClientRepository.Insert(obje);
        var result1 = await _unitOfWork.SaveAsync();
        if (result1 == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "Resgisteration Success",
            data = null
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "Resgisteration Faild",
            data = null
          };
        }
      }
      catch (Exception ex)
      {

        return new Result
        {
          success = false,
          code = "403",
          message = "Row Added Faild",
          data = null
        };
      }
    }
    #endregion


    public Boolean FindCustomerBySocailId(string socialId)
    {
      var res = _unitOfWork.EsSrClientRepository.Get(filter: (x => x.SocialId == socialId));
      if (res != null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #region UpdateClient
    public async Task<Result> UpdateClient(EsSrClientViewModel esSrClientViewModel)
    {
      try
      {
        EsSrClient esSrClient = new EsSrClient();
        var obje = _mapper.Map(esSrClientViewModel, esSrClient);
        obje.IsDelete = false;
        obje.IsActive = true;
        obje.ModifiedOn = DateTime.Now;
        _unitOfWork.EsSrClientRepository.Update(obje);
        var result1 = await _unitOfWork.SaveAsync();
        if (result1 == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            message = "Update Client Faild",
            data = null
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "403",
            message = "Update Client Faild",
            data = null
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };
      }

    }
    #endregion
  }
}
