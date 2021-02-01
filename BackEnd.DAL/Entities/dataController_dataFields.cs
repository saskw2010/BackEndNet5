using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class dataController_dataFields
  {
    public long FieldId { get; set; }
    public string FieldName { get; set; }
    public string AliasFieldName { get; set; }
    public string Columns { get; set; }
    [ForeignKey("dataController_views")]
    public Nullable<long> ViewFkId { get; set; }
    public virtual dataController_views dataController_views { get; set; }
  }
}
