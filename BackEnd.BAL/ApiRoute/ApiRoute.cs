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
      public const string RegisterMobile = Base + "/Identity/RegisterMobile";
      public const string Roles = Base + "/Identity/Roles";
      public const string verfayUser = Base + "Identity/verfayUser";
      public const string ResendVerficationCode = Base + "Identity/ResendVerficationCode";
      public const string GetUser = Base + "Identity/GetUser";
      public const string GetUserById = Base + "Identity/GetUserById";
      public const string GetAll = Base + "Identity/GetAll";
      public const string AddUser = Base + "Identity/AddUser";
      public const string DeleteUser = Base + "Identity/DeleteUser";
      public const string UpdateUser = Base + "Identity/UpdateUser";
      public const string GetUserByUserName = Base + "Identity/GetUserByUserName";
      public const string GetByUserId = Base + "Identity/GetByUserId";
      public const string RestPassword = Base + "Identity/RestPassword";
    }

    public static class WebSite
    {
      public const string CreateWebsite = Base + "webSite/CreateWebsite";
      public const string CreateWebsiteV2 = Base + "webSite/CreateWebsiteV2";
      public const string Get = Base + "webSite/Get";
     public const string Filter = Base + "webSite/filter";
     public const string CheckAvailability = Base + "webSite/CheckAvailability";
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
    //EsSrClient
    public static class Client
    {
      public const string CreateCLient = Base + "EsSrClient/CreateCLient";
      public const string createUserForClients = Base + "EsSrClient/createUserForClients";
      public const string socialRegister = Base + "EsSrClient/socialRegister";
      public const string UpdateCLient = Base + "EsSrClient/UpdateCLient";
    }

    public static class Technical
    {
      public const string createUserForTechnicals = Base + "EsSrTchnical/createUserForTechnicals";
      public const string createUserForTechnicalsWithhashing = Base + "EsSrTchnical/createUserForTechnicalsWithhashing";
      public const string GetAvailabelTechncails = Base + "EsSrTchnical/GetAvailabelTechncails";
    }

    //WorkshopRegion
    public static class EsSrWorkshop
    {
      public const string checkAvailabelItem = Base + "EsSrWorkshopRegion/checkAvailabelTechncails";
    }
    //EsSrOrderRouting
    public static class EsSrOrderRouting
    {
      public const string saveOrder = Base +    "EsSrOrderRouting/saveOrder";
      public const string UpdateOrder = Base + "EsSrOrderRouting/UpdateOrder";
    }

    public static class EsSrOrderStageRoute
    {
      public const string UpdateEsSrOrderStage = Base + "EsSrOrderStage/UpdateEsSrOrderStage";
    }

  }
}
