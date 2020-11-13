using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service.ISercice;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.V1
{
 
  public class IdentityController: Controller
  {
    private readonly Random _random = new Random();
    private IidentityServices _identityService;
    
    public IdentityController(IidentityServices identityServices,
      BakEndContext Context)
    {
      _identityService = identityServices;
      
    }
    [HttpPost(ApiRoute.Identity.Register)]
    public async Task<Result> Register([FromBody] UserRegisterationRequest request) {
      if (!ModelState.IsValid) {
        ModelState.Values.SelectMany(x=>x.Errors.Select(xx=>xx.ErrorMessage));
        return new Result {
          success=false,
          data= ModelState.Values
        };
        
        
      }
      AuthenticationResult authResponse = await _identityService.RegisterAsync(request.UserName, request.Email, request.PhoneNumber, request.Password, request.Roles);
      if (!authResponse.Success) {
        var res = new AuthFaildResponse
        {
          success = false,
          Errors = authResponse.Errors
        };
        return new Result
        {
          success = true,
          data = res
        };

      }

      var res2=new AuthSuccessResponse {
        success = true,
        Token =authResponse.Token
      };
      return new Result
      {
        success = true,
        data = res2
      };

    }

    [HttpPost(ApiRoute.Identity.Login)]
    public async Task<Result> Login([FromBody] UserLoginRequest request)
    {
        var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
        if (!authResponse.Success)
        {
        var res =
            new AuthFaildResponse
            {
              success = false,
              Errors = authResponse.Errors
            };
        return new Result
        {
          success=false,
          data=res
        };
           
        }
        else {
        var res = await _identityService.CheckverfayUserByEmail(request.Email);
        if (res.success == true)
        {
          var res2=new AuthSuccessResponse
          {
            success = true,
            Token = authResponse.Token
          };
          return new Result {
            success=true,
            data= res2
          };
        }
        else {
          var res3=new AuthFaildResponse
          {
            success = true,
            Errors = new List<string>() {
              res.message
            },
            confirmd=false
          };
          return new Result
          {
            success = true,
            data = res3
          };
        }
      
      }

    }

    [HttpGet(ApiRoute.Identity.Roles)]
    public Result GetAllROles() {
      return _identityService.getAllRoles();
    }


    [HttpPost(ApiRoute.Identity.verfayUser)]
    public async Task<Result> verfayUser([FromBody] UserVerfayRequest request)
    {
     Result res=await _identityService.verfayUser(request);
      return res;
    }

    [HttpPost(ApiRoute.Identity.ResendVerficationCode)]
    public async Task<Result> ResendVerficationCode([FromBody] UserVerfayRequest request)
    {
      if (!ModelState.IsValid)
      {
        ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
        var res1=ModelState.Values;
        return new Result {
          success=false,
          data=res1
        };

      }
      int num = _random.Next();
      Result res = await _identityService.updateVerficationCode(num, request.Email);
      if (res.success == true) {
       await _identityService.sendVerficationToEMail(num, request.Email);
      }
      return res;
    }

    [HttpGet(ApiRoute.Identity.GetUser)]
    public async Task<Result> GetUser(string Email) {
      var res=await _identityService.getUserByEmail(Email);
      return res;
    }





  }
}
