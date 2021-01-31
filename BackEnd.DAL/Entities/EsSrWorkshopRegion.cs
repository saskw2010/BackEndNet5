using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrWorkshopRegion
  {
    [Key]
   public long WorkshopRegionId { get; set; }
   [ForeignKey("EsSrWorkshop")]
    public Nullable<long> WorkshopId { get; set; }
    public Nullable<long> PicStockId { get; set; }
    public string Code { get; set; }
    public string NameEn { get; set; }
    public string NameAr{ get; set; }
    public Nullable<decimal> MapLatitude { get; set; }
    public Nullable<decimal> MapLangitude { get; set; }
    public Nullable<decimal> MapArea { get; set; }
    public string Phone { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string Email2 { get; set; }
    public Nullable<int> MaxNumOfTeam { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<EsSrTechnical> EsSrTechnicals { get; set; }
    public virtual ICollection<EsSrPeriodTechnical> EsSrPeriodTechnical { get; set; }
    public virtual EsSrWorkshop EsSrWorkshop { get; set; }
  }
}
