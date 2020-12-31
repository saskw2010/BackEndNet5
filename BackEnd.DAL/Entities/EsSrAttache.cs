using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrAttache
  {
    [Key]
    public long AttacheId { get; set; }
    [ForeignKey("EsSrOrder")]
    public Nullable<long> OrderId { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public Nullable<long> Length { get; set; }
    public string ControllerName { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public virtual EsSrOrder EsSrOrder { get; set; }

  }
}
