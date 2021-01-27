using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class xmlController
  {
    [Key]
    public int Id { get; set; }
    public string dataControllerCollection_xmlns { get; set; }
    public string dataControllerCollection_snapshot { get; set; }
    public string dataControllerCollection_Name { get; set; }
    public string dataControllerCollection_Version { get; set; }
    public virtual ICollection<XmlFile> XmlFile { get; set; }
  }
}
