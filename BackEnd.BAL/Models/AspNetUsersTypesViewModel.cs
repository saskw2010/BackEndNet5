using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class AspNetUsersTypesViewModel
  {
    public long UsrTypID { get; set; }
    public string UsrTypNm { get; set; }
    public string UsrTypNm1 { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

  }
}
