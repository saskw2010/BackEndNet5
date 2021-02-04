using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IEsSrClientService
  {
    Task<Result> CreateCLient(EsSrClientViewModel esSrClientViewModel);
    Task<Result> CreateCLientWithSocialId(EsSrClientViewModel esSrClientViewModel);
    Task<Result> UpdateClient(EsSrClientViewModel esSrClientViewModel);
    Task<Result>  createUserForClientsAsync();
    Task<Result> GetCLientByEmail(string email);
    Task<Result> UpdateCLientFbToken(string userName, string FbToken);

  }
}
