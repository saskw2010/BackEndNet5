using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class WorkSpaceVm
  {
    public int Id { get; set; }
    public string WorkSpaceName { get; set; }
    public string UserName { get; set; }
    public string DatabaseName { get; set; }
    public string UserId { get; set; }
  }
}
