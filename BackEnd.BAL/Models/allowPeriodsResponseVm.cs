using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class allowPeriodsResponseVm
  {
    public DateTime Date { get; set; }
    public List<PeriodVm> PeriodVm { get; set; }
  }
}
