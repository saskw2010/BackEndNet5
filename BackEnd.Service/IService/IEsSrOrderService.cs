using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IEsSrOrderService
  {
    Task<Result> saveOrder(EsSrOrderViewModel esSrOrderVm);
    Task<Result> UpdateOrder(EsSrOrderViewModel esSrOrderVm);
  }
}
