using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class EsSrClient
  {
    [Key]
    public long ClientId { get; set; }
    public Nullable<long> ClientTypeId { get; set; }
    public Nullable<long> CityId { get; set; }
    public Nullable<long> PicStockId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string Email2 { get; set; }
    public string FbToken { get; set; }
    public string HasPassword { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public string CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public Nullable<bool> IsDelete { get; set; }
    public string SocialId { get; set; }
  }
}
