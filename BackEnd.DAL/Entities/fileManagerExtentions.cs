using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class fileManagerExtentions
  {
    [Key]
    public int Id { get; set; }
    public string extention { get; set; }
  }
}
