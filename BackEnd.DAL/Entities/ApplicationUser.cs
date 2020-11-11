using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
      public int? verficationCode { get; set; }
      public Boolean? confirmed { get; set; }
    public int? resetPasswordCode { get; set; }
    public virtual ICollection<WorkSpace> WorkSpace { get; set; }
  }
}
