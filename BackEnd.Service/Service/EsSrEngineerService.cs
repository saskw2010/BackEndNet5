using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.BAL.Repository;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class EsSrEngineerService : IEsSrEngineerService
  {
    private IUnitOfWork _unitOfWork;
    private readonly BakEndContext _BakEndContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private IMapper _mapper;
    public IConfiguration Configuration { get; }
    public EsSrEngineerService(IUnitOfWork unitOfWork,
      BakEndContext BakEndContext,
      UserManager<ApplicationUser> userManager,
      IConfiguration Iconfig,
      IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _BakEndContext = BakEndContext;
      _userManager = userManager;
      Configuration = Iconfig;
      _mapper = mapper;
    }
    #region createUserForSupervisorAsync
    public async Task<Result> createUserForEngineerAsync()
    {
      try
      {
        List<EsSrEngineer> esSrEnginsers = _unitOfWork.EsSrEngineerRepository.Get().ToList();
        foreach (var item in esSrEnginsers)
        {
          var User = FindByEmailCustome(item.Email);
          User = FindByUserNameCustom(item.Email);
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
          message = ExtensionMethods.FullMessage(ex)
        };
      }
    }
    #endregion

    #region FindByEmailCustome
    public ApplicationUser FindByEmailCustome(string email)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.Email == email);
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
        PasswordHash = EncodePasswordmosso(Password),
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
        LastLockoutDate = DateTime.Now,
      };

      await _userManager.CreateAsync(newUser);
      //-----------------------------add Role to token------------------
      await _userManager.AddToRoleAsync(newUser, "enginner");
      //-----------------------------------------------------------------
    }

    #endregion

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
    #region FindByUserNameCustom
    public ApplicationUser FindByUserNameCustom(string userName)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.UserName == userName);
    }
    #endregion
  }
}
