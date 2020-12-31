using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrOrder
  {
    [Key]
    public long OrderId { get; set; }
    public Nullable<long> ItemId { get; set; }
    public Nullable<long> PromoCodeId { get; set; }
    public Nullable<long> ClientId { get; set; }
    public Nullable<long> OrderStageId { get; set; }
    public Nullable<decimal> MapLatitude { get; set; }
    public Nullable<decimal> MapLangitude { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ProviderNotes { get; set; }
    public Nullable<decimal> Amount { get; set; }
    public Nullable<decimal> Discount { get; set; }
    public Nullable<decimal> PaidUp { get; set; }
    public Nullable<System.DateTime> OrderDate { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public Nullable<long> CityId { get; set; }
    [ForeignKey("EsSrPeriodTechnical")]
    public Nullable<long> PeriodTechnicalId { get; set; }
    public Nullable<bool> IsApproval { get; set; }
    public string MapAddress { get; set; }
    public string AddressDetail { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<EsSrAttache> EsSrAttaches { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
 
    public virtual EsSrPeriodTechnical EsSrPeriodTechnical { get; set; }
   
  }
}
