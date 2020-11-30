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
  public class RoleController: ControllerBase
  {
    private IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
      _roleService = roleService;
    }
    [HttpGet(ApiRoute.Role.GetAllAspNetUserType)]
    public Result GetAllAspNetUserType() {
     return _roleService.getAllAspNetUserType();
    }

  }
}
