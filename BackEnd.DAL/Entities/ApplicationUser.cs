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
      public Boolean? EmailConfirmed { get; set; }
      public Boolean? IsApproved { get; set; }
      public Boolean? PhoneNumberConfirmed { get; set; }
     public DateTime creationDate { get; set; }
     public DateTime lastLoginDate { get; set; }
     public DateTime lastActivityDate { get; set; }
     public DateTime lastPasswordChangedDate { get; set; }
     public DateTime lastLockedOutDate { get; set; }
    public int? resetPasswordCode { get; set; }
      [ForeignKey("UserType")]
    public int? userTypeId { get; set; }
    public virtual UserType UserType { get; set; }
    public virtual ICollection<WorkSpace> WorkSpace { get; set; }
    public virtual List<AspNetusertypjoin> AspNetusertypjoin { get; set; }
  }
}
