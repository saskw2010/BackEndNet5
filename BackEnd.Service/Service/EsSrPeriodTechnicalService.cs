using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.Service
{
  public class EsSrPeriodTechnicalService : IEsSrPeriodTechnicalService
  {
    private IUnitOfWork _unitOfWork;
    public EsSrPeriodTechnicalService(IUnitOfWork unitOfWork
      )
    {
      _unitOfWork = unitOfWork;
   
    }

    public long GetTechnicalPeriodIdofLessNumberOfOrder(List<PeriodTechnicalsVm> periodTechnicalsVm, DateTime orderDate)
    {
      
     var ListOfOrders= _unitOfWork.EsSrOrderRepository.Get(filter: (x => (x.IsActive == true) &&(x.IsDelete== false)));
      ListOfOrders = ListOfOrders.Where(x => checkDate(x.OrderDate, orderDate));
      long maxTechnicalPeriodId = periodTechnicalsVm.FirstOrDefault().PeriodTechnicalId;
      int maxNumberOfOrder = 0;
      foreach (var element in periodTechnicalsVm) {
        int countOfOrders = ListOfOrders.Count(x=>x.PeriodTechnicalId == element.PeriodTechnicalId);        
        if (maxNumberOfOrder < countOfOrders && countOfOrders < element.maxNumberOfOrders)
        {
          maxNumberOfOrder = countOfOrders;
          maxTechnicalPeriodId = element.PeriodTechnicalId;
        }
      }
      return maxTechnicalPeriodId;
    }

    private Boolean checkDate(DateTime? day1, DateTime? day2)
    {
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
      else
      {
        return false;
      }

    }

  }
}
