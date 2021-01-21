using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL.Models;
using Microsoft.AspNetCore.Mvc;
using BackEnd.BAL.ApiRoute;

namespace BackEnd.Web.Controllers
{
  public class EsSrEngineerController : ControllerBase
  {
    private IRoleService _roleService;
    private IEsSrEngineerService _EsSrEngineerService;
    public EsSrEngineerController(IEsSrEngineerService EsSrEngineerService,
      IRoleService roleService)
    {
      _EsSrEngineerService = EsSrEngineerService;
      _roleService = roleService;
    }

    
    #region createUserForEnginner
    [HttpPost(ApiRoute.Engineer.createUserForEnginner)]
    public async Task<Result> createUserForEnginner()
    {
      await _roleService.createRoleOfNotExist("enginner");
      return await _EsSrEngineerService.createUserForEngineerAsync();
    }
    #endregion
  }
}
