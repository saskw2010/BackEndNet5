using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using BackEnd.Web.Extensions;
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

    #region AddspNetUsersTypes_roles
    [HttpPost(ApiRoute.Role.AddspNetUsersTypes_roles)]
    public async Task<Result> AddspNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel)
    {
      string UserId = HttpContext.getUserId();
      aspNetUsersTypes_rolesViewModel.CreatedBy = UserId;
      return await _roleService.AddspNetUsersTypes_roles(aspNetUsersTypes_rolesViewModel);
    }
    #endregion

    //#region UpdatespNetUsersTypes_roles
    //[HttpPut(ApiRoute.Role.UpdatespNetUsersTypes_roles)]
    //public async Task<Result> UpdatespNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel)
    //{
    //  string UserId = HttpContext.getUserId();
    //  aspNetUsersTypes_rolesViewModel.ModifiedBy = UserId;
    //  return await _roleService.UpdatespNetUsersTypes_roles(aspNetUsersTypes_rolesViewModel);
    //}
    //#endregion

    #region DeletespNetUsersTypes_roles
    [HttpDelete(ApiRoute.Role.DeletespNetUsersTypes_roles)]
    public async Task<Result> DeletespNetUsersTypes_roles(string IdAspNetRoles,long UsrTypID) {
      return await _roleService.DeleteAspNetUsersTypesRoles(IdAspNetRoles, UsrTypID);
    }
    #endregion
  }
}
