using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class EsSrClientController: ControllerBase
  {
    private IEsSrClientService _EsSrClientService;
    private IidentityServices _identityService;
    private IRoleService _roleService;
    public EsSrClientController(
      IEsSrClientService EsSrClientService,
      IidentityServices identityServices,
      IRoleService roleService)
    {
      _EsSrClientService = EsSrClientService;
      _identityService = identityServices;
      _roleService = roleService;
    }

    #region CreateCLient
    [HttpPost(ApiRoute.Client.CreateCLient)]
    public async Task<Result> CreateCLient([FromBody] EsSrClientViewModel request)
    {
      if (!ModelState.IsValid)
      {
       
        return new Result
        {
          success = false,
          code="401",
          data = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
      };


      }
      await _roleService.createRoleOfNotExist("Client");
      var res1 = await _EsSrClientService.CreateCLient(request);
      if (res1.success == true)
      {
        var res2 = await RegisterMobile(request.Email,request.Phone,request.HasPassword);
        return res2;
      }
      return res1;
    }
    #endregion

    #region RegisterMobile
    private async Task<Result> RegisterMobile(string Email,string PhoneNumber,string Password)
    {
      if (!ModelState.IsValid)
      {
        return new Result
        {
          success = false,
          code="403",
          data = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
        };


      }
      AuthenticationResult authResponse = await _identityService.RegisterMobileAsync(Email, Email, PhoneNumber, Password);
      if (!authResponse.Success)
      {
        var res = new AuthFaildResponse
        {
          success = false,
          Errors = authResponse.Errors
        };
        return new Result
        {
          success = true,
          code="403",
          data = res
        };

      }

      var res2 = new AuthSuccessResponse
      {
        success = true,
        Token = authResponse.Token
      };
      return new Result
      {
        success = true,
        data = res2
      };

    }
    #endregion

    #region createUserForClients
    [HttpPost(ApiRoute.Client.createUserForClients)]
    public async Task<Result> createUserForClients() {
      await _roleService.createRoleOfNotExist("Client");
      return await _EsSrClientService.createUserForClientsAsync();
    }
    #endregion



    #region socialRegister
    [HttpPost(ApiRoute.Client.socialRegister)]
    public async Task<Result> socialRegister([FromBody] EsSrClientViewModel request)
    {
      await _roleService.createRoleOfNotExist("Client");
      var res1 = await _EsSrClientService.CreateCLientWithSocialId(request);
      if (res1.success == true)
      {
        var res2 = await _identityService.socialRegister(request.SocialId, request.Email, request.Email, request.Phone, request.HasPassword);
        return res2;
      }
      return res1;
    }
    #endregion


  }
}
