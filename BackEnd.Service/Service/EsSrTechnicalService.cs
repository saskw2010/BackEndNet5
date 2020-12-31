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
  public class EsSrTechnicalService : IEsSrTechnicalService
  {
    private IUnitOfWork _unitOfWork;
    private readonly BakEndContext _BakEndContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private IMapper _mapper;
    public IConfiguration Configuration { get; }
    public EsSrTechnicalService(IUnitOfWork unitOfWork,
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
    #region createUserForTechnicalAsync
    public async Task<Result> createUserForTechnicalAsync()
    {
      try
      {
        List<EsSrTechnical> esSrTechnical = _unitOfWork.EsSrTechnicalRepository.Get().ToList();
        foreach (var item in esSrTechnical)
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


    #region createUserForTechnicalsWithhashing
    public async Task<Result> createUserForTechnicalsWithhashing()
    {
      try
      {
        List<EsSrTechnical> esSrTechnical = _unitOfWork.EsSrTechnicalRepository.Get().ToList();
        foreach (var item in esSrTechnical)
        {
          var password = item.HashPassword;
          item.HashPassword = EncodePasswordmosso(password);
          _unitOfWork.EsSrTechnicalRepository.Update(item);
          await _unitOfWork.SaveAsync();
          var User = FindByEmailCustome(item.Email);
          if (User == null)
          {
            await addUser(item.Email, item.Email, item.Phone,EncodePasswordmosso(password));
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
    #region createUserForTechnicalAsync
    public async Task<Result> createUserForTechnicalHashingAsync()
    {
      try
      {
        List<EsSrTechnical> esSrTechnical = _unitOfWork.EsSrTechnicalRepository.Get().ToList();
        foreach (var item in esSrTechnical)
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
        LastLockoutDate = DateTime.Now,
      };

      await _userManager.CreateAsync(newUser);
      //-----------------------------add Role to token------------------
      await _userManager.AddToRoleAsync(newUser, "Technical");
      //-----------------------------------------------------------------
    }

    #endregion

    #region GetAvailabelTechncails
    public async Task<Result> GetAvailabelTechncails(AllowedTechViewMode allowedTechViewMode)
    {
      List<EsSrTechnical> AllTechncalsForWorkSopRegion = new List<EsSrTechnical>();
      List<EsSrTechnicalViewModel> AllTechncalsForWorkSopRegionVm = new List<EsSrTechnicalViewModel>();
      foreach (var workShopRItem in allowedTechViewMode.esSrWorkshopRegionViewModel)
      {
        var EsSrTechnicalList = _unitOfWork.EsSrTechnicalRepository.Get(filter: (x =>
          (x.WorkshopRegionId == workShopRItem.WorkshopRegionId) &&
          (x.EsSrItemTechnicals.Any(estItem => estItem.ItemId == allowedTechViewMode.ItemId))));
        AllTechncalsForWorkSopRegion.AddRange(EsSrTechnicalList);
      }
      List<allowPeriodsResponseVm> ss = new List<allowPeriodsResponseVm>();
      ss = FilterEsSrPeriodTechnicalAllowed(AllTechncalsForWorkSopRegion, allowedTechViewMode.dateFrom,allowedTechViewMode.dateTo);
      AllTechncalsForWorkSopRegionVm = _mapper.Map<List<EsSrTechnicalViewModel>>(AllTechncalsForWorkSopRegion);
      return new Result { success=true,data = ss };
    }
    #endregion

    #region FilterEsSrPeriodTechnicalAllowed
    private List<allowPeriodsResponseVm> FilterEsSrPeriodTechnicalAllowed(List<EsSrTechnical> essrTechList, DateTime fromDateReques, DateTime todateReques)
    {
      List<allowPeriodsResponseVm> allowPeriodsReVm = new List<allowPeriodsResponseVm>();
      List<PeriodVm> pvm = new List<PeriodVm>();
      
      foreach (var item in essrTechList)
      {
        //all period lok
        //var EsSrPeriodTechnicals=_unitOfWork.EsSrPeriodTechnicalRepository.Get(filter: (x => x.TechnicalId == item.TechnicalId));
        
        foreach (var periodTechnical in item.EsSrPeriodTechnicals.Select(x=>new { EsSrPeriodLocks=x.EsSrPeriodLocks,
          PeriodId = x.PeriodId }))
        {
          foreach (DateTime day in EachDay(fromDateReques, todateReques))
          {
            
            var x=periodTechnical.EsSrPeriodLocks.Select(s=>new
            { EsSrPeriod = s.EsSrPeriod,
              FromDate=s.FromDate,
              ToDate=s.ToDate
            }
            ).Where(x => (x.FromDate <= day) && (x.ToDate >= day));
            if (x.Count() == 0)
            {
              //EsSrPeriodAllowed.Add(periodTechnical.PeriodId.Value);
             var period= _unitOfWork.EsSrPeriodRepository.GetEntity(x=>x.PeriodId== periodTechnical.PeriodId.Value);
              var tecVm= getTechnicalByPeriodId(period.PeriodId);
              PeriodVm obj = new PeriodVm
              { PireodId= periodTechnical.PeriodId.Value,
              NameAr= period.NameAr,
              NameEn=period.NameEn,
              PeriodTechnicalsVm= tecVm
              };
              pvm.Add(obj);
              
              allowPeriodsResponseVm allowedVm = new allowPeriodsResponseVm {
                Date=day,
              PeriodVm= pvm
              };
              allowPeriodsReVm.Add(allowedVm);
            }
          }

        }
        
      }
      return  allowPeriodsReVm;
    }
    #endregion

    public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
      for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
        yield return day;
    }

    #region getTechnicalByPeriodId
    public List<PeriodTechnicalsVm> getTechnicalByPeriodId(long periodId) {
      List<PeriodTechnicalsVm> PeriodTechnicalsVm = new List<PeriodTechnicalsVm>();
     var res= _unitOfWork.EsSrPeriodTechnicalRepository.Get(filter:(x=> x.PeriodId!= null?x.PeriodId.Value == periodId:false));
      foreach (var item in res) {
        PeriodTechnicalsVm tcVm = new PeriodTechnicalsVm();
        tcVm.PeriodTechnicalId = item.PeriodTechnicalId;
        PeriodTechnicalsVm.Add(tcVm);
      }
      return PeriodTechnicalsVm;
    }
    #endregion

  }
}
