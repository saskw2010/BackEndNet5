using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrOrderStageBaseCatgeory
  {
    [Key]
    public long OrderStageBaseCatgeoryId { get; set; }
    [ForeignKey("EsSrOrderStageBase")]
    public Nullable<long> OrderStageBaseId { get; set; }
    public Nullable<long> CatgeoryId { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }

    public virtual EsSrOrderStageBase EsSrOrderStageBase { get; set; }
  }
}
