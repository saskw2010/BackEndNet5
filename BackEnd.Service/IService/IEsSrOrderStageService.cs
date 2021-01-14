using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IEsSrOrderStageService
  {
    Task<Result> UpdateEsSrOrderStage(EsSrOrderStageViewModel esSrOrderViewModel);
  }
}
