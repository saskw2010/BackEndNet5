using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
  public interface IEsSrPeriodTechnicalService
  {
    long GetTechnicalPeriodIdofLessNumberOfOrder(List<PeriodTechnicalsVm> periodTechnicalsVm,DateTime orderDate);
  }
}
