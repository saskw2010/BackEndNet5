using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class AddUserResult
  {
    public Boolean Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public string UserId { get; set; }
  }
}
