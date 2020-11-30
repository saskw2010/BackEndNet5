using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class AspNetUsersTypes
  {
      [Key]
      public long UsrTypID { get; set; }
      public string UsrTypNm { get; set; }
      public string UsrTypNm1 { get; set; }
      public string ModifiedBy { get; set; }
      public DateTime ModifiedOn { get; set; }
      public string CreatedBy { get; set; }
      public DateTime CreatedOn { get; set; }
     public virtual List<AspNetusertypjoin> AspNetusertypjoin { get; set; }
      public virtual List<AspNetUsersTypes_roles> AspNetUsersTypes_roles { get; set; }
  }
}
