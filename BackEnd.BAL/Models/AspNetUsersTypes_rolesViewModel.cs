using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class AspNetUsersTypes_rolesViewModel
  {
    public string IdAspNetRoles { get; set; }
    public string IdentityRole { get; set; }
    public long UsrTypID { get; set; }
    public string AspNetUsersTypes { get; set; }

    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }


  }
}
