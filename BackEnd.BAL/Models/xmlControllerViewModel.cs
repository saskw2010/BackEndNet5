using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class xmlControllerViewModel
  {
    public int Id { get; set; }
    public string dataControllerCollection_xmlns { get; set; }
    public string dataControllerCollection_snapshot { get; set; }
    public string dataControllerCollection_Name { get; set; }
    public string dataControllerCollection_Version { get; set; }
  }
}
