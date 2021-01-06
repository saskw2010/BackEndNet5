using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class EsSrOrderViewModel
  {
    public long? CatgeoryId { get; set; }
    public long? OrderId { get; set; }
    public int? OrderStageShowOrder { get; set; }
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
    public Nullable<long> PeriodTechnicalId { get; set; }
    public Nullable<bool> IsApproval { get; set; }
    public string MapAddress { get; set; }
    public string AddressDetail { get; set; }
  }
}
