using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.ISercice
{
  public interface IidentityServices
  {
    Task<AuthenticationResult> RegisterAsync(string UserName,string Email, string PhoneNumber,string Password, string Roles);
    Task<AuthenticationResult> RegisterMobileAsync(string UserName,string Email, string PhoneNumber,string Password);
    Task<Result> socialRegister(string socialCode, string UserName, string Email, string PhoneNumber, string Password);
    Task<AddUserResult> AddUserAsync(string UserName,string Email, string PhoneNumber,string Password);
    Task<AuthenticationResult> LoginAsync(string Email,string UserName, string Password);
    Task<Result> verfayUser(UserVerfayRequest request);
    Task<Result> CheckverfayUserByEmail(string userName);
    Task<Boolean> sendVerficationToEMail(int verficationCode, string Email);
    Task<Result> updateVerficationCode(int num,string Email);
    Task<Result> getUserByEmail(string Email);
    Result GetUserByUserName(string userName);
    Task<Result> updateresetPasswordCodeCode(int num,string Email);
    Result getAllRoles();
    Task<Result> getUserById(string UserId);
    Task<Result> getUserAndUserUserTypeByUserId(string UserId);

    Task<Result> pagginationUser(string searchWord,int pageNumber, int pageSize);
    Task<Result> DeleteUser(string UserId);
    Task<UpdateUserResult> UpdateUser(string userId, string UserName, string Email, string PhoneNumber, string Password);
    bool RegexIsMatch(string password);
    Task<Result> RestPassword(RestPasswordViewModel restPasswordViewModel);
  }
}
