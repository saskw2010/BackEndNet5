using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.ApiRoute
{
  public static class ApiRoute
  {
    public const string Root= "api";
    public const string Version = "v1";
    public const string Base =Root+"/"+Version;

    public static class Identity
    {
      public const string Login = Base + "Identity/Login";
      public const string Register = Base + "/Identity/Register";
      public const string Roles = Base + "/Identity/Roles";
      public const string verfayUser = Base + "Identity/verfayUser";
      public const string ResendVerficationCode = Base + "Identity/ResendVerficationCode";
      public const string GetUser = Base + "Identity/GetUser";
      public const string GetUserById = Base + "Identity/GetUserById";
      public const string GetAll = Base + "Identity/GetAll";
      public const string AddUser = Base + "Identity/AddUser";
    }

    public static class WebSite
    {
      public const string CreateWebsite = Base + "webSite/CreateWebsite";
      public const string CreateWebsiteV2 = Base + "webSite/CreateWebsiteV2";
      public const string Get = Base + "webSite/Get";
     public const string Filter = Base + "webSite/filter";
    }

    public static class Role
    {
      public const string GetAllAspNetUserType = Base + "Role/GetAllAspNetUserType";
      public const string GetAllAspNetUsersTypes_roles = Base + "Role/GetAllAspNetUsersTypes_roles";
      public const string AddspNetUsersTypes_roles = Base + "Role/AddspNetUsersTypes_roles";
      public const string UpdatespNetUsersTypes_roles = Base + "Role/UpdatespNetUsersTypes_roles";
      public const string DeletespNetUsersTypes_roles = Base + "Role/DeletespNetUsersTypes_roles";
      public const string FilterAspNetUsersTypes_roles = Base + "Role/FilterAspNetUsersTypes_roles";
    }


  }
}
