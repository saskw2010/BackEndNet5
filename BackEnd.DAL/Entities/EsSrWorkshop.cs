using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrWorkshop
  {
    [Key]
    public long WorkshopId { get; set; }
    public Nullable<long> CityId { get; set; }
    public Nullable<long> PicStockId { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionAr { get; set; }
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
    public string Code { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public Nullable<long> OrganisationId { get; set; }
    public virtual ICollection<EsSrWorkshopRegion> EsSrWorkshopRegions { get; set; }
  }
}
