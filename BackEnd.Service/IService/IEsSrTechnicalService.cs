using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IEsSrTechnicalService
  {
    Task<Result> createUserForTechnicalAsync();
    Task<Result> createUserForTechnicalsWithhashing();
    Task<Result> GetAvailabelTechncails(AllowedTechViewMode allowedTechViewMode);
    Task<Result> UpdateTechnical(EsSrTechnicalViewModel esSrTechnicalViewModel);

    
  }
}
