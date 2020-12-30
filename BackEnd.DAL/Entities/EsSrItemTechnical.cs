using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrItemTechnical
  {
    [Key]
    public long ItemTeamId { get; set; }
    public Nullable<long> ItemId { get; set; }
    [ForeignKey("EsSrTechnical")]
    public Nullable<long> TechnicalId { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }

    public virtual EsSrTechnical EsSrTechnical { get; set; }
  }
}
