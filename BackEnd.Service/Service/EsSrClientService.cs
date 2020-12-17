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

namespace BackEnd.Service.Service
{
  public class EsSrClientService : IEsSrClientService
  {
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public IConfiguration Configuration { get; }
    private readonly RoleManager<IdentityRole> _roleManager;
    public EsSrClientService(IUnitOfWork unitOfWork, IMapper mapper,
      IConfiguration iConfig,
      RoleManager<IdentityRole> roleManager)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      Configuration = iConfig;
      _roleManager = roleManager;
    }
    public async Task<Result> CreateCLient(EsSrClientViewModel esSrClientViewModel)
    {
      try
      {
        if (checEmailALreadyExist(esSrClientViewModel.Email)) {
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
      catch (Exception ex) {

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
    public Boolean checEmailALreadyExist(string email) {
     var res= _unitOfWork.EsSrClientRepository.GetEntity(filter:(x=>x.Email == email));
      if (res != null) {
        return true;
      }
      else {
        return false;
      }
    }
    #endregion
  }
}
