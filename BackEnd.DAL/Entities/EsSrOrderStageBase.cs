using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrOrderStageBase
  {
    [Key]
    public long OrderStageBaseId { get; set; }
    public Nullable<long> OrganisationId { get; set; }
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
    public Nullable<bool> MustClientApprove { get; set; }
    public Nullable<bool> MustProviderApprove { get; set; }
    public Nullable<bool> ClientApprove { get; set; }
    public Nullable<bool> ProviderApprove { get; set; }
    public string ClientNotes { get; set; }
    public string ProviderNotes { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<EsSrOrderStageBaseCatgeory> EsSrOrderStageBaseCatgeories { get; set; }
  }
}
