using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.Service
{
    public class EsSrPeriodTechnicalService : IEsSrPeriodTechnicalService
    {
        private IUnitOfWork _unitOfWork;
        public EsSrPeriodTechnicalService(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;
   
        }

        public long? GetTechnicalPeriodIdofLessNumberOfOrder(EsSrOrderViewModel EsSrOrdervm, List<PeriodTechnicalsVm> periodTechnicalsVm, DateTime orderDate)
        {
      
            var ListOfOrders= _unitOfWork.EsSrOrderRepository.Get(filter: (x => (x.IsActive == true) &&(x.IsDelete== false)));
            ListOfOrders = ListOfOrders.Where(x => checkDate(x.OrderDate, orderDate));
            long maxTechnicalPeriodId = periodTechnicalsVm.FirstOrDefault().PeriodTechnicalId;
            int maxNumberOfOrder = 0;
            foreach (var element in periodTechnicalsVm)
            {
              var workShopRegion = _unitOfWork.EsSrWorkshopRegionRepository.GetByID(element.WorkshopRegionId);
               decimal distanceBettweenOrderAndWorkShopRegion= findDistanceBetweenTwoCoordinates(EsSrOrdervm.MapLangitude, EsSrOrdervm.MapLatitude, workShopRegion.MapLangitude,workShopRegion.MapLangitude);
              if (workShopRegion.MapArea >= distanceBettweenOrderAndWorkShopRegion) {
                int countOfOrders = ListOfOrders.Count(x => x.PeriodTechnicalId == element.PeriodTechnicalId);
                if (maxNumberOfOrder < countOfOrders && countOfOrders < element.maxNumberOfOrders)
                {
                  maxNumberOfOrder = countOfOrders;
                  maxTechnicalPeriodId = element.PeriodTechnicalId;
                }
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
    decimal findDistanceBetweenTwoCoordinates(decimal? langFromRequest, decimal? latFromRequest, decimal? lanFromWrkShop, decimal? latFromWorkShop)
    {
      var coord = new GeoCoordinate(Convert.ToDouble(langFromRequest), Convert.ToDouble(latFromRequest));
      var dist = coord.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(lanFromWrkShop), Convert.ToDouble(latFromWorkShop)));
      return Convert.ToDecimal(dist);
    }

  }
}
