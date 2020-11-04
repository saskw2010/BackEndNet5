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
  }
}
