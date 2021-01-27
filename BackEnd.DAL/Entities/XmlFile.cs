using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public class XmlFile
  {
    [Key]
    public int Id { get; set; }
    public string DataModelXml { get; set; }
    public string DataControllerXml { get; set; }
    public string CompanyName { get; set; }
    public string Version { get; set; }
    [ForeignKey("xmlController")]
    public int CoontrollerFkId { get; set; }
    public string FileName { get; set; }
    public virtual xmlController xmlController { get; set; }

  }
}
