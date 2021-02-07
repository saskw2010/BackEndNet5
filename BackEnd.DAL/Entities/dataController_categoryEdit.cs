using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class dataController_categoryEdit
  {
    [Key]
    public long CategoryId { get; set; }
    public string headerText { get; set; }
    public string flow { get; set; }
    public string description { get; set; }
    [ForeignKey("dataController_views")]
    public Nullable<long> ViewFkId { get; set; }

    public virtual dataController_views dataController_views { get; set; }
    public virtual ICollection<dataController_dataFieldEdit> dataController_dataFieldEdit { get; set; }

  }
}
