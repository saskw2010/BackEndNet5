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
    public EsSrOrderController(IEsSrOrderService EsSrOrderService)
    {
      _EsSrOrderService = EsSrOrderService;
    }
    #region saveOrder
    [HttpPost(ApiRoute.EsSrOrderRouting.saveOrder)]
    public async Task<Result> saveOrder([FromBody] EsSrOrderViewModel esSrOrderVm) {
      return await _EsSrOrderService.saveOrder(esSrOrderVm);
    }
    #endregion

    #region UpdateOrder
    [HttpPut(ApiRoute.EsSrOrderRouting.UpdateOrder)]
    public async Task<Result> UpdateOrder([FromBody] EsSrOrderViewModel esSrOrderVm)
    {
      return await _EsSrOrderService.UpdateOrder(esSrOrderVm);
    }
    #endregion
    
  }
}
