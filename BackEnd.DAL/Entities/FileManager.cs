using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class FileManager
  {
    [Key]
    public int Id { get; set; }
    public string filemanagerName { get; set; }
    public virtual List<FileManagerRole> FileManagerRole { get; set; }
  }
}
