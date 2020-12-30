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
  public class EsSrWorkshopRegionController : ControllerBase
  {
    private IEsSrWorkshopRegionServices _EsSrWorkshopRegionServices;
    public EsSrWorkshopRegionController(IEsSrWorkshopRegionServices EsSrWorkshopRegionServices)
    {
      _EsSrWorkshopRegionServices = EsSrWorkshopRegionServices;
    }
    [HttpGet(ApiRoute.EsSrWorkshop.checkAvailabelItem)]
    public  Result checkAvailabelItem(long itemId, decimal lng, decimal lat)
    {
      return _EsSrWorkshopRegionServices.checkAvailabelItem(itemId, lng, lat);
    }
  }
}
