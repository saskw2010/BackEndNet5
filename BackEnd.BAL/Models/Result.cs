using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class Result
  {
    public Boolean success { get; set; }
    public string code { get; set; }
    public string message { get; set; }
    public object Data { get; set; }
  }
}
