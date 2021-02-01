using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class PeriodTechnicalsVm
  {
    public long PeriodTechnicalId { get; set; }
    public int? maxNumberOfOrders { get; set; }
    public long? WorkshopRegionId { get; set; }
    public long? PeriodId { get; set; }
    public decimal Distance { get; set; }
    public int countOfOrder { get; set; }
  }
}
