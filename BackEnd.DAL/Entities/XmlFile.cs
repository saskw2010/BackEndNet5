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
    public string CountryFkId { get; set; }
    public xmlController xmlController { get; set; }

  }
}
