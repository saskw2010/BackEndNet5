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
    public EsSrTechnicalController(
      IRoleService roleService,
      IEsSrTechnicalService EsSrTechnicalService
      )
    {
      _roleService = roleService;
      _EsSrTechnicalService = EsSrTechnicalService;
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
  }
}
