using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrPeriod
  {
    [Key]
    public long PeriodId { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionAr { get; set; }
    public Nullable<System.TimeSpan> FromTime { get; set; }
    public Nullable<System.TimeSpan> ToTime { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public string Code { get; set; }
    public Nullable<long> OrganisationId { get; set; }
  public virtual ICollection<EsSrPeriodLock> EsSrPeriodLocks { get; set; }
    public virtual ICollection<EsSrPeriodTechnical> EsSrPeriodTechnicals { get; set; }
  }
}
