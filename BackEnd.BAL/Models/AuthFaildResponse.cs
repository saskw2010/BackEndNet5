using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthFaildResponse
  {
    public Boolean success { get; set; }
    public IEnumerable<string> Errors { get; set; }
  }
}
