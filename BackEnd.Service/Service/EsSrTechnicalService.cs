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
            await addUser(item.Email, item.Email, item.Phone, EncodePasswordmosso(password));
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
      try
      {
        List<EsSrTechnical> AllTechncalsForWorkSopRegion = new List<EsSrTechnical>();
        List<EsSrTechnicalViewModel> AllTechncalsForWorkSopRegionVm = new List<EsSrTechnicalViewModel>();
        foreach (var workShopRItem in allowedTechViewMode.esSrWorkshopRegionViewModel)
        {
          var EsSrTechnicalList = _unitOfWork.EsSrTechnicalRepository.Get(filter: (x =>
            (x.WorkshopRegionId == workShopRItem.WorkshopRegionId) &&
            (x.EsSrItemTechnicals.Any(estItem => estItem.ItemId == allowedTechViewMode.ItemId))
            &&(x.IsActive == true) && (x.IsDelete == false)));
          AllTechncalsForWorkSopRegion.AddRange(EsSrTechnicalList);
        }
        
        List<allowPeriodsResponseVm> ss = new List<allowPeriodsResponseVm>();
        ss = FilterEsSrPeriodTechnicalAllowed(AllTechncalsForWorkSopRegion, allowedTechViewMode.dateFrom, allowedTechViewMode.dateTo);
        AllTechncalsForWorkSopRegionVm = _mapper.Map<List<EsSrTechnicalViewModel>>(AllTechncalsForWorkSopRegion);
        return new Result { success = true, data = ss };
    }
      catch (Exception ex) {
        return new Result { success = false,code="400", data = null,message= ExtensionMethods.FullMessage(ex)
  };
}
     
    }
    #endregion

    #region FilterEsSrPeriodTechnicalAllowed
    private List<allowPeriodsResponseVm> FilterEsSrPeriodTechnicalAllowed(List<EsSrTechnical> essrTechList, DateTime fromDateReques, DateTime todateReques)
    {
      List<allowPeriodsResponseVm> allowPeriodsReVm = new List<allowPeriodsResponseVm>();
      List<PeriodVm> pvm = new List<PeriodVm>();
      List<EsSrPeriodTechnical> esSrPeriodTechnicalsList = new List<EsSrPeriodTechnical>();
      foreach (var item in essrTechList)
      {
        var addedPeriod = item.EsSrPeriodTechnicals.Where(x=>(x.IsDelete==false && x.IsActive == true));
        if (addedPeriod != null)
          esSrPeriodTechnicalsList.AddRange(addedPeriod);
      }
      var periods = esSrPeriodTechnicalsList.Select(x => new
      {
        PireodId = x.PeriodId.Value,
        NameAr = x.EsSrPeriod.NameAr,
        NameEn = x.EsSrPeriod.NameEn,
        fromTime = x.EsSrPeriod.FromTime,
        toTime = x.EsSrPeriod.ToTime,
        WorkshopRegionId = x.WorkshopRegionId,
        EsSrPeriodLock = x.EsSrPeriod.EsSrPeriodLocks.Where(x=>x.IsDelete == false && x.IsActive == true),
        EsSrTechnicalWorkDays = x.EsSrTechnical.EsSrTechnicalWorkDays.Where(x => x.IsDelete == false && x.IsActive == true),
        PeriodTechnicalsVm = esSrPeriodTechnicalsList.Where(y => (y.PeriodId == x.PeriodId)&&(y.IsActive == true) && (y.IsDelete == false)).ToList(),
        maximumNumberOfOrderBerTechnical = x.MaxNumOfOrder,
        sumMax = esSrPeriodTechnicalsList.Sum(y=>y.MaxNumOfOrder),
        EsSrOrders = x.EsSrOrders

      }).GroupBy(sc => new { sc.PireodId }).Select(g => g.First()).ToList();
      bool flag = true;
      foreach (DateTime day in EachDay(fromDateReques, todateReques))
      {
        
       
        foreach (var period in periods)
        {
          if (flag)
          {
            if (period.fromTime != null && fromDateReques.Hour >= period.fromTime.Value.Hours)
            {
              continue;
            }
          }
          var x = period.EsSrPeriodLock.Where(x => (x.PeriodTechnicalId == null)&&(x.FromDate <= day) && (x.ToDate >= day));
          
          if (x.Count() == 0)
          {
            var tecVm = new List<PeriodTechnicalsVm>();
            foreach (var periodTechnical in period.PeriodTechnicalsVm)
            {
              var y = periodTechnical.EsSrPeriodLocks.Where(x =>(x.PeriodTechnicalId == null)&&(x.FromDate <= day) && (x.ToDate >= day));
              if (y.Count() == 0)
              {
                tecVm.Add(new PeriodTechnicalsVm() {
                    PeriodTechnicalId = periodTechnical.PeriodTechnicalId,
                    maxNumberOfOrders=periodTechnical.MaxNumOfOrder,
                    WorkshopRegionId = periodTechnical.WorkshopRegionId
                });
              }
            }
            if (tecVm.Count > 0 ) {
              

             var worksDay= period.EsSrTechnicalWorkDays.FirstOrDefault(x=>x.WorkDaysId == (long)day.DayOfWeek);
              //-------------------get Order By date------------------
              var countOderOfDay = period.EsSrOrders.Where(x=>checkDate(x.OrderDate, day.Date));
              if ((countOderOfDay.Count() <= period.maximumNumberOfOrderBerTechnical) && (worksDay != null)) {
                PeriodVm obj = new PeriodVm
                {
                  PireodId = period.PireodId,
                  NameAr = period.NameAr,
                  NameEn = period.NameEn,
                  PeriodTechnicalsVm = tecVm
                };
                pvm.Add(obj);
              }

              
            }
           
          }
          
        }

        if (pvm.Count > 0) {
          allowPeriodsResponseVm allowedVm = new allowPeriodsResponseVm
          {
            Date = day,
            PeriodVm = pvm,
            dayOfWork = day.DayOfWeek
          };
          allowPeriodsReVm.Add(allowedVm);
        }
        
        pvm = new List<PeriodVm>();
        flag = false;
      }
      return allowPeriodsReVm;
    }
    #endregion
    public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
      for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
        yield return day.AddHours(12);
    }
    #region checkDate
    private Boolean checkDate(DateTime? day1, DateTime? day2) {
      if (day1 != null && day2 != null)
      {
        if ((day1.Value.Day == day2.Value.Day) && (day1.Value.Month == day2.Value.Month) && (day1.Value.Year == day2.Value.Year))
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else {
        return false;
      }
      
    }
    #endregion

    #region UpdateTechnical
    public async Task<Result> UpdateTechnical(EsSrTechnicalViewModel esSrTechnicalViewModel)
    {
      try
      {
        EsSrTechnical esSrClient = new EsSrTechnical();
        var obje = _mapper.Map(esSrTechnicalViewModel, esSrClient);
        obje.IsDelete = false;
        obje.IsActive = true;
        obje.ModifiedOn = DateTime.Now;
        _unitOfWork.EsSrTechnicalRepository.Update(obje);
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
    #region FindByUserNameCustom
    public ApplicationUser FindByUserNameCustom(string userName)
    {
      return _BakEndContext.Users.FirstOrDefault(x => x.UserName == userName);
    }
    #endregion



  }
}
