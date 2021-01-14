using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class EsSrOrderStageViewModel
  {
    public long OrderStageId { get; set; }
    public Nullable<long> OrderId { get; set; }
    public string DescriptionEn { get; set; }
    public string DescriptionAr { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public Nullable<decimal> AdditionalCost { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public Nullable<int> ProgressValue { get; set; }
    public Nullable<bool> MustClientApprove { get; set; }
    public Nullable<bool> MustProviderApprove { get; set; }
    public Nullable<bool> ClientApprove { get; set; }
    public Nullable<bool> ProviderApprove { get; set; }
    public string ClientNotes { get; set; }
  }
}
