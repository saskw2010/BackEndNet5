using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class FileManagerRole
  {
    [Key]
    public int Id { get; set; }
    [ForeignKey("FileManager")]
    public int fileMangerFkId { get; set; }
    public virtual FileManager FileManager { get; set; }

    [ForeignKey("IdentityRole")]
    public string aspNetRoleFkId { get; set; }
    public virtual IdentityRole IdentityRole { get; set; }
  }
}
