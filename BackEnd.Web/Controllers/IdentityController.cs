using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using BackEnd.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
 
  public class IdentityController: ControllerBase
  {
    private readonly Random _random = new Random();
    private IidentityServices _identityService;
    private IRoleService _roleService;
    public IdentityController(
      IidentityServices identityServices,
      IRoleService roleService)
    {
      _identityService = identityServices;
      _roleService = roleService;
    }
    #region Register
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
    #endregion

    #region Login
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
    #endregion

    #region Role
    [HttpGet(ApiRoute.Identity.Roles)]
    public Result GetAllROles() {
      return _identityService.getAllRoles();
    }
    #endregion

    #region verfayUser
    [HttpPost(ApiRoute.Identity.verfayUser)]
    public async Task<Result> verfayUser([FromBody] UserVerfayRequest request)
    {
     Result res=await _identityService.verfayUser(request);
      return res;
    }
    #endregion

    #region ResendVerficationCode
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
    #endregion

    #region GetUser
    [HttpGet(ApiRoute.Identity.GetUser)]
    public async Task<Result> GetUser(string Email) {
      var res=await _identityService.getUserByEmail(Email);
      return res;
    }
    #endregion

    #region GetUserById
    [HttpGet(ApiRoute.Identity.GetUserById)]
    public async Task<Result> GetUserById()
    {
      string UserId = HttpContext.getUserId();
      var res = await _identityService.getUserById(UserId);
      return res;
    }
    #endregion

    #region GetAll
    [HttpGet(ApiRoute.Identity.GetAll)]
    public async Task<Result> GetAll(string searchWord,int pageNumber=1, int pageSize=5)
    {
      var res = await _identityService.pagginationUser(searchWord,pageNumber, pageSize);
      return res;
    }
    #endregion

    #region AddUser
    [HttpPost(ApiRoute.Identity.AddUser)]
    public async Task<Result> AddUser([FromBody] UserViewModel userAddViewModel)
    {
      //add User
      if (!ModelState.IsValid)
      {
        ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
        return new Result
        {
          success = false,
          data = ModelState.Values
        };


      }
      AddUserResult addedResult = await _identityService.AddUserAsync(userAddViewModel.UserName, userAddViewModel.Email, userAddViewModel.PhoneNumber, userAddViewModel.Password);
      if (!addedResult.Success)
      {
        var res = new AuthFaildResponse
        {
          success = false,
          Errors = addedResult.Errors
        };
        return new Result
        {
          success = true,
          data = res
        };

      }
      //end add User
      var res2 = await _roleService.AddAspNetUserTypeJoin(userAddViewModel.aspNetUsersTypesViewModel, addedResult.UserId);
      if (res2.success == true)
      {
        return new Result
        {
          success = true,
          code = "200",
          message = "Verfication Code Sent To Email Successfuly"
        };
      }
      else {
        return new Result
        {
          success = true,
          code = "403",
          message = "Add User Faild"
        };
      }
    }

    #endregion

    #region deleteUser
    [HttpDelete(ApiRoute.Identity.DeleteUser)]
    public async Task<Result> DeleteUser(string UserId) {
        await _identityService.DeleteUser(UserId);
        var res2 = await _roleService.RemoveAspNetUserTypeJoin(UserId);
        return res2;
     
      
       
    }
    #endregion

    #region UpdateUser
    [HttpPut(ApiRoute.Identity.UpdateUser)]
    public async Task<Result> UpdateUser([FromBody] UserViewModel userUpdateViewModel)
    {
      if (!ModelState.IsValid)
      {
        ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
        return new Result
        {
          success = false,
          data = ModelState.Values
        };


      }
      UpdateUserResult UpdateResult = await _identityService.UpdateUser(userUpdateViewModel.Id,userUpdateViewModel.UserName,userUpdateViewModel.Email,userUpdateViewModel.PhoneNumber,userUpdateViewModel.Password);
      if (!UpdateResult.Success)
      {
        var res = new AuthFaildResponse
        {
          success = false,
          Errors = UpdateResult.Errors
        };
        return new Result
        {
          success = true,
          data = res
        };
      }
      //end add User
      //--Beign::Remove old AspNetUsetTypeJoin
     var res11=await _roleService.RemoveAspNetUserTypeJoin(UpdateResult.UserId);
      //-----
      if (res11.success == true)
      {
        var res2 = await _roleService.AddAspNetUserTypeJoin(userUpdateViewModel.aspNetUsersTypesViewModel, UpdateResult.UserId);
        if (res2.success == true)
        {
          return new Result
          {
            success = true,
            code = "200",
            message= "Verfication Code Sent To Email Successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = true,
            code = "403",
            message = "update User Faild"
          };
        }

      }
      else {
        return new Result
        {
          success = false,
          code = "403",
          message = "uodate User Faild"
        };
      }
    }

    #endregion

    #region GetByUserId
    [HttpGet(ApiRoute.Identity.GetByUserId)]
    public async Task<Result> GetByUserId(string UserId) {
      var res = await _identityService.getUserAndUserUserTypeByUserId(UserId);
      return res;
    }
    #endregion
  }
}
