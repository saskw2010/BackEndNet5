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
    Task<AuthenticationResult> LoginAsync(string Email, string Password);
    Task<Result> verfayUser(int verficationCode);
    Task<Result> CheckverfayUserByEmail(string Email);
    Task<Boolean> sendVerficationToEMail(int verficationCode, string Email);
    Task<Result> updateVerficationCode(int num,string Email);
    Task<Result> getUserByEmail(string Email);
    Task<Result> updateresetPasswordCodeCode(int num,string Email);
    Result getAllRoles();
  }
}
