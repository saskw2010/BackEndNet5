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
    #region GetAllAspNetUserType
    [HttpGet(ApiRoute.Role.GetAllAspNetUserType)]
    public Result GetAllAspNetUserType() {
     return _roleService.getAllAspNetUserType();
    }
    #endregion

    #region GetAllAspNetUsersTypes_roles
    [HttpGet(ApiRoute.Role.GetAllAspNetUsersTypes_roles)]
    public Result GetAllAspNetUsersTypes_roles(int pageNumber = 1, int pageSize = 2)
    {
      return _roleService.GetAllAspNetUsersTypes_roles(pageNumber , pageSize );
    }
    #endregion
  }
}
