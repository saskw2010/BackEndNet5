using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
 public interface IEsSrEngineerService
  {
    Task<Result> createUserForEngineerAsync();
  }
}
