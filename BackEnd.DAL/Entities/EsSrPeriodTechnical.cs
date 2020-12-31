using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrPeriodTechnical
  {
    [Key]
    public long PeriodTechnicalId { get; set; }
    [ForeignKey("EsSrTechnical")]
    public Nullable<long> TechnicalId { get; set; }
    [ForeignKey("EsSrPeriod")]
    public Nullable<long> PeriodId { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public string Code { get; set; }

    public virtual ICollection<EsSrOrder> EsSrOrders { get; set; }
    public virtual EsSrPeriod EsSrPeriod { get; set; }
    public virtual ICollection<EsSrPeriodLock> EsSrPeriodLocks { get; set; }
    public virtual EsSrTechnical EsSrTechnical { get; set; }
  }
}
