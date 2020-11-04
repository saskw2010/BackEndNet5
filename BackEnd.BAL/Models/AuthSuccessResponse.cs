using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthSuccessResponse
  {
    public Boolean success { get; set; }
    public string Token { get; set; }
  }
}
