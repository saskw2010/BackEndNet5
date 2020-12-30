using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
  public interface IEsSrWorkshopRegionServices
  {
    Result checkAvailabelItem(long itemId, decimal lng, decimal lat);
  }
}
