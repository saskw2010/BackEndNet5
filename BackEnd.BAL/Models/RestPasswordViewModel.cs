using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class RestPasswordViewModel
  {
    public string UserName { get; set; }
    public string NewPassword { get; set; }
    public string  OldPassword{ get; set; }
  }
}
