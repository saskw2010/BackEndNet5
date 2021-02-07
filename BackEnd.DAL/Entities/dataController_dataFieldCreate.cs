using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class dataController_dataFieldCreate
  {
    [Key]
    public long FieldId { get; set; }
    public string FieldName { get; set; }
    public string AliasFieldName { get; set; }
    public string Columns { get; set; }
    [ForeignKey("dataController_categoryCreate")]
    public Nullable<long> categoryCreateFkId { get; set; }

    public virtual dataController_categoryCreate dataController_categoryCreate { get; set; }

  }
}
