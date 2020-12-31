using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class AllowedTechViewMode
  {
    public long ItemId { get; set; }
    public List<EsSrWorkshopRegionViewModel> esSrWorkshopRegionViewModel { get; set; }
    public DateTime dateFrom { get; set; }
    public DateTime dateTo { get; set; }
  }
}
