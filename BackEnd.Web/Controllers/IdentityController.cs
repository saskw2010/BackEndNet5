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
    private readonly BakEndContext _BakEndContext;
    public IdentityController(IidentityServices identityServices,
      BakEndContext Context)
    {
      _identityService = identityServices;
      _BakEndContext = Context;
    }
    [HttpPost(ApiRoute.Identity.Register)]
    public async Task<IActionResult> Register([FromBody] UserRegisterationRequest request) {
      if (!ModelState.IsValid) {
        ModelState.Values.SelectMany(x=>x.Errors.Select(xx=>xx.ErrorMessage));
        return Ok(ModelState.Values);

      }
      AuthenticationResult authResponse = await _identityService.RegisterAsync(request.UserName, request.Email, request.PhoneNumber, request.Password, request.Roles);
      if (!authResponse.Success) {
        return Ok(
            new AuthFaildResponse {
              success=false,
              Errors = authResponse.Errors
            }
          );
      }

      return Ok(new AuthSuccessResponse {
        success = true,
        Token =authResponse.Token
      });
    }

    [HttpPost(ApiRoute.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
        if (!authResponse.Success)
        {
          return Ok(
              new AuthFaildResponse
              {
                success = false,
                Errors = authResponse.Errors
              }
            );
        }
        else {
        var res = await _identityService.CheckverfayUserByEmail(request.Email);
        if (res.success == true)
        {
          return Ok(new AuthSuccessResponse
          {
            success = true,
            Token = authResponse.Token
          });
        }
        else {
          return Ok(new AuthFaildResponse
          {
            success = true,
            Errors = new List<string>() {
              res.message
            },
            confirmd=false
          }) ;
        }
      
      }

    }

    [HttpGet(ApiRoute.Identity.Roles)]
    public List<IdentityRole> GetAllROles() {
      return _BakEndContext.Roles.ToList();
    }


    [HttpPost(ApiRoute.Identity.verfayUser)]
    public async Task<ActionResult> verfayUser([FromBody] UserVerfayRequest request)
    {
     Result res=await _identityService.verfayUser(request.verficationCode);
      return Ok(res);
    }

    [HttpPost(ApiRoute.Identity.ResendVerficationCode)]
    public async Task<ActionResult> ResendVerficationCode([FromBody] UserVerfayRequest request)
    {
      if (!ModelState.IsValid)
      {
        ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
        return Ok(ModelState.Values);

      }
      int num = _random.Next();
      Result res = await _identityService.updateVerficationCode(num, request.Email);
      if (res.success == true) {
       await _identityService.sendVerficationToEMail(num, request.Email);
      }
      return Ok(res);
    }






  }
}
