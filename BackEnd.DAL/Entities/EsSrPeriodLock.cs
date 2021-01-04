using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrPeriodLock
  {
    [Key]
    public long PeriodLockId { get; set; }
    [ForeignKey("EsSrPeriodTechnical")]
    public Nullable<long> PeriodTechnicalId { get; set; }
    [ForeignKey("EsSrPeriod")]
    public Nullable<long> PeriodId { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionAr { get; set; }
    public Nullable<System.DateTime> FromDate { get; set; }
    public Nullable<System.DateTime> ToDate { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }

    public virtual EsSrPeriod EsSrPeriod { get; set; }
    public virtual EsSrPeriodTechnical EsSrPeriodTechnical { get; set; }
  }
}
