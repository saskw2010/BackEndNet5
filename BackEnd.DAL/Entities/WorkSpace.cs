using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class WorkSpace
  {
    [Key]
    public int Id { get; set; }
    public string WorkSpaceName { get; set; }
    public string UserName { get; set; }
    public string DatabaseName { get; set; }
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
  }
}
