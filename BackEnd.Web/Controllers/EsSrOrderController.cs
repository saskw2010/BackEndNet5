using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    public class EsSrOrderController : ControllerBase
    {
        #region privateFilde
        private IEsSrOrderService _EsSrOrderService;
        private IEsSrPeriodTechnicalService _EsSrPeriodTechnicalService;
        #endregion

        #region EsSrOrderController
        public EsSrOrderController(
          IEsSrOrderService EsSrOrderService,
          IEsSrPeriodTechnicalService EsSrPeriodTechnicalService)
        {
            _EsSrOrderService = EsSrOrderService;
            _EsSrPeriodTechnicalService = EsSrPeriodTechnicalService;
        }
        #endregion

        #region saveOrder
        [HttpPost(ApiRoute.EsSrOrderRouting.saveOrder)]
        public async Task<Result> saveOrder([FromBody] EsSrOrderViewModel esSrOrderVm)
      {
       try { 
            var order = _EsSrPeriodTechnicalService.GetTechnicalPeriodIdofLessNumberOfOrder(esSrOrderVm);
            //esSrOrderVm.PeriodTechnicalId = PeriodTehnicalId;
            return await _EsSrOrderService.saveOrder(order);
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

    #region UpdateOrder
    [HttpPost(ApiRoute.EsSrOrderRouting.UpdateOrder)]
    public async Task<Result> UpdateOrder([FromBody] EsSrOrderViewModel esSrOrderVm)
    {
      try
      {
        //return List Of PeriodTechnial
        if (esSrOrderVm.periodTechnicalsVm != null)
        {
          var order = _EsSrPeriodTechnicalService.GetTechnicalPeriodIdofLessNumberOfOrder(esSrOrderVm);
          return await _EsSrOrderService.UpdateOrder(order);
        }
        else
        {
          return await _EsSrOrderService.UpdateOrder(esSrOrderVm);
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
