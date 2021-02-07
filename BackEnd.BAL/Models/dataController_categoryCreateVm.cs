using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class dataController_categoryCreateVm
  {
    public long CategoryId { get; set; }
    public string headerText { get; set; }
    public string flow { get; set; }
    public string description { get; set; }
    public Nullable<long> ViewFkId { get; set; }
    public List<dataController_dataFieldEditVm> dataController_dataFieldEdit { get; set; }
  }
}
