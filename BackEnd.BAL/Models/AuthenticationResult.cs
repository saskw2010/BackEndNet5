using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthenticationResult
  {
    public string Token { get; set; }
    public Boolean Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
  }
}
