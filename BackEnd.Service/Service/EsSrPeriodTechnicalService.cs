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
        #region privateFilde
        private IUnitOfWork _unitOfWork;
        #endregion

        #region EsSrPeriodTechnicalService
        public EsSrPeriodTechnicalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetTechnicalPeriodIdofLessNumberOfOrder
        public EsSrOrderViewModel GetTechnicalPeriodIdofLessNumberOfOrder(EsSrOrderViewModel EsSrOrdervm)
        {

            var ListOfOrders = _unitOfWork.EsSrOrderRepository.Get(filter: (x => checkDate(x.OrderDate, EsSrOrdervm.OrderDate) && (x.IsCanceled == false) &&(x.IsActive == true) && (x.IsDelete == false)));
            //ListOfOrders = ListOfOrders.Where(x => checkDate(x.OrderDate, EsSrOrdervm.OrderDate));
            //long maxTechnicalPeriodId = EsSrOrdervm.periodTechnicalsVm.FirstOrDefault().PeriodTechnicalId;
            //int maxNumberOfOrder = 0;
            foreach (var element in EsSrOrdervm.periodTechnicalsVm)
            {
                var workShopRegion = _unitOfWork.EsSrWorkshopRegionRepository.GetByID(element.WorkshopRegionId);
                element.Distance = findDistanceBetweenTwoCoordinates(EsSrOrdervm.MapLangitude, EsSrOrdervm.MapLatitude, workShopRegion.MapLangitude, workShopRegion.MapLangitude);
                element.countOfOrder = ListOfOrders.Count(x => x.PeriodTechnicalId == element.PeriodTechnicalId);
                if (workShopRegion.MapArea >= element.Distance)
                {
                    if (element.maxNumberOfOrders <= element.countOfOrder)
                    {
                        element.countOfOrder = int.MaxValue;
                    }
                }
                else
                {
                    element.Distance = decimal.MaxValue;
                }
            }
            var tp = EsSrOrdervm.periodTechnicalsVm.Where(x => x.countOfOrder != int.MaxValue && x.Distance != decimal.MaxValue);//.OrderBy(x => x.countOfOrder);
            if (tp != null && tp.Count() > 0)
            {
                //tp = tp.OrderBy(x => x.Distance).OrderBy(y => y.countOfOrder);
                var select = tp.OrderBy(x => x.Distance).OrderBy(y => y.countOfOrder).FirstOrDefault();
                EsSrOrdervm.WorkshopRegionId = select.WorkshopRegionId;
                EsSrOrdervm.PeriodId = select.PeriodId;
                EsSrOrdervm.PeriodTechnicalId = select.PeriodTechnicalId;

            }
            else
            {
                var select = EsSrOrdervm.periodTechnicalsVm.Where(x => x.Distance != decimal.MaxValue).OrderBy(x => x.Distance).FirstOrDefault();
                if (select == null)
                {
                    select = EsSrOrdervm.periodTechnicalsVm.FirstOrDefault();
                }
                EsSrOrdervm.WorkshopRegionId = select.WorkshopRegionId;
                EsSrOrdervm.PeriodId = select.PeriodId;
            }
            return EsSrOrdervm;
        }
        #endregion

        #region checkDate
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
        #endregion

        #region findDistanceBetweenTwoCoordinates
        decimal findDistanceBetweenTwoCoordinates(decimal? langFromRequest, decimal? latFromRequest, decimal? lanFromWrkShop, decimal? latFromWorkShop)
        {
            var coord = new GeoCoordinate(Convert.ToDouble(langFromRequest), Convert.ToDouble(latFromRequest));
            var dist = coord.GetDistanceTo(new GeoCoordinate(Convert.ToDouble(lanFromWrkShop), Convert.ToDouble(latFromWorkShop)));
            return Convert.ToDecimal(dist);
        }
        #endregion
    }
}
