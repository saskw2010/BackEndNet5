using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
      public int? verficationCode { get; set; }
      public Boolean? confirmed { get; set; }
      public int? resetPasswordCode { get; set; }
      [ForeignKey("UserType")]
    public int? userTypeId { get; set; }
    public virtual UserType UserType { get; set; }
    public virtual ICollection<WorkSpace> WorkSpace { get; set; }
  }
}
