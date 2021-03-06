using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrTechnical
  {
    [Key]
    public long TechnicalId { get; set; }
    [ForeignKey("EsSrWorkshopRegion")]
    public Nullable<long> WorkshopRegionId { get; set; }
    public Nullable<long> PicStockId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string Email2 { get; set; }
    public string HashPassword { get; set; }
    public string FbToken { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public Nullable<int> MaxNumOfOrder { get; set; }
    public string Code { get; set; }
    public virtual ICollection<EsSrItemTechnical> EsSrItemTechnicals { get; set; }
    public virtual EsSrWorkshopRegion EsSrWorkshopRegion { get; set; }
    public virtual ICollection<EsSrPeriodTechnical> EsSrPeriodTechnicals { get; set; }
    public virtual ICollection<EsSrTechnicalWorkDays> EsSrTechnicalWorkDays { get; set; }

  }
}
