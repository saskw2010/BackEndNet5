using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IRoleService
  {
    Result getAllAspNetUserType();
    Result GetAllAspNetUsersTypes_roles(int pageNumber = 1, int pageSize = 2);
   Task<Result> AddspNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel);
  // Task<Result> UpdatespNetUsersTypes_roles(AspNetUsersTypes_rolesInsertViewModel aspNetUsersTypes_rolesViewModel);
   Task<Result> DeleteAspNetUsersTypesRoles(string IdAspNetRoles, long UsrTypID);
   Task<Result> FilterAspNetUsersTypes_roles(string SerachWord, int pageNumber = 1, int pageSize = 2);
    Task<Result> AddAspNetUserTypeJoin(List<AspNetUsersTypesViewModel> aspNetUsersTypesViewModel,string idAspNetUser);
    Task<Result> RemoveAspNetUserTypeJoin(string idAspNetUser);
    Task createRoleOfNotExist(string RoleName);
  }
}
