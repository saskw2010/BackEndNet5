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
  public class EsSrOrderController: ControllerBase
  {
    private IEsSrOrderService _EsSrOrderService;
    private IEsSrPeriodTechnicalService _EsSrPeriodTechnicalService;
    public EsSrOrderController(
      IEsSrOrderService EsSrOrderService,
      IEsSrPeriodTechnicalService EsSrPeriodTechnicalService)
    {
      _EsSrOrderService = EsSrOrderService;
      _EsSrPeriodTechnicalService = EsSrPeriodTechnicalService;
    }
    #region saveOrder
    [HttpPost(ApiRoute.EsSrOrderRouting.saveOrder)]
    public async Task<Result> saveOrder([FromBody] EsSrOrderViewModel esSrOrderVm) {
      var PeriodTehnicalId = _EsSrPeriodTechnicalService.GetTechnicalPeriodIdofLessNumberOfOrder(esSrOrderVm,esSrOrderVm.periodTechnicalsVm, esSrOrderVm.OrderDate.Value);
      esSrOrderVm.PeriodTechnicalId = PeriodTehnicalId;
      return await _EsSrOrderService.saveOrder(esSrOrderVm);
    }
    #endregion

    #region UpdateOrder
    [HttpPost(ApiRoute.EsSrOrderRouting.UpdateOrder)]
    public async Task<Result> UpdateOrder([FromBody] EsSrOrderViewModel esSrOrderVm)
    {
      //return List Of PeriodTechnial
      
      return await _EsSrOrderService.UpdateOrder(esSrOrderVm);
    }
    #endregion
    
  }
}
