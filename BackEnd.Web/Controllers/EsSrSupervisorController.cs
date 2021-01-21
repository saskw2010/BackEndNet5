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
  public class EsSrSupervisorController: ControllerBase
  {
    private IRoleService _roleService;
    private IEsSrSupervisorService _EsSrSupervisorService;
    public EsSrSupervisorController(IEsSrSupervisorService EsSrSupervisorService,
      IRoleService roleService)
    {
      _EsSrSupervisorService = EsSrSupervisorService;
      _roleService = roleService;
    }

    
    #region createUserForSupervisor
    [HttpPost(ApiRoute.SuperVisor.createUserForSuperVisor)]
    public async Task<Result> createUserForSupervisor()
    {
      await _roleService.createRoleOfNotExist("supervisor");
      return await _EsSrSupervisorService.createUserForSupervisorAsync();
    }
    #endregion
  }
}
