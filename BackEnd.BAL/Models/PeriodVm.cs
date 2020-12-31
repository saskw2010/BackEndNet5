using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class PeriodVm
  {
    public long PireodId { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public List<PeriodTechnicalsVm> PeriodTechnicalsVm { get; set; }
  }
}
