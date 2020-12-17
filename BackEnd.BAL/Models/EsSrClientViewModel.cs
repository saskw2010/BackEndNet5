using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class EsSrClientViewModel
  {
    public long TechnicalId { get; set; }
    public Nullable<long> WorkshopRegionId { get; set; }
    public Nullable<long> PicStockId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Phone2 { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Email2 { get; set; }
    public string HasPassword { get; set; }
    public string FbToken { get; set; }
    public Nullable<int> ShowOrder { get; set; }
    public Nullable<bool> IsActive { get; set; }
    public string Notes { get; set; }
    public Nullable<int> MaxNumOfOrder { get; set; }
    public string Code { get; set; }
  }
}
