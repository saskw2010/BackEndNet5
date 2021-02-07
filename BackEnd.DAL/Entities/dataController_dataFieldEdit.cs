using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class dataController_dataFieldEdit
  {
    [Key]
    public long FieldId { get; set; }
    public string FieldName { get; set; }
    public string AliasFieldName { get; set; }
    public string Columns { get; set; }
    [ForeignKey("dataController_categoryEdit")]
    public Nullable<long> categoryEditFkId { get; set; }
    public virtual dataController_categoryEdit dataController_categoryEdit { get; set; }

  }
}
