using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class AspNetusertypjoin
  {
    [Key]
    [ForeignKey("ApplicationUser")]
    public string IdAspNetUsers { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    [Key]
    [ForeignKey("AspNetUsersTypes")]
    public long UsrTypID { get; set; }
    public virtual AspNetUsersTypes AspNetUsersTypes { get; set; }

    public string ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
