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
  public class EsSrTechnicalController: ControllerBase
  {
    private IRoleService _roleService;
    private IEsSrTechnicalService _EsSrTechnicalService;
    private IEsSrWorkshopRegionServices _EsSrWorkshopRegionServices;
    public EsSrTechnicalController(
      IRoleService roleService,
      IEsSrTechnicalService EsSrTechnicalService,
      IEsSrWorkshopRegionServices EsSrWorkshopRegionServices
      )
    {
      _roleService = roleService;
      _EsSrTechnicalService = EsSrTechnicalService;
      _EsSrWorkshopRegionServices = EsSrWorkshopRegionServices;
    }

    #region createUserForTechnicals
    [HttpPost(ApiRoute.Technical.createUserForTechnicals)]
    public async Task<Result> createUserForClients()
    {
      await _roleService.createRoleOfNotExist("Technical");
      return await _EsSrTechnicalService.createUserForTechnicalAsync();
    }
    #endregion

    #region createUserForClients
    [HttpPost(ApiRoute.Technical.createUserForTechnicalsWithhashing)]
    public async Task<Result> createUserForTechnicalsWithhashing()
    {
      await _roleService.createRoleOfNotExist("Technical");
      return await _EsSrTechnicalService.createUserForTechnicalsWithhashing();
    }
    #endregion

    #region checkAvailabelTechncails
    [HttpPost(ApiRoute.Technical.GetAvailabelTechncails)]
    public async Task<Result> GetAvailabelTechncails([FromBody]AllowedTechViewMode allowedTechViewMode) {
      //get technical of thes Regions
      return await _EsSrTechnicalService.GetAvailabelTechncails(allowedTechViewMode);
      
    }
    #endregion

  }
}
