using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class dataController_dataFieldCreateVm
  {
    public long FieldId { get; set; }
    public string FieldName { get; set; }
    public string AliasFieldName { get; set; }
    public string Columns { get; set; }
  }
}
