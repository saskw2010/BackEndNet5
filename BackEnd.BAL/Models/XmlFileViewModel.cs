using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.BAL.Models
{
  public class XmlFileViewModel
  {
    public int Id { get; set; }
    public string DataModelXml { get; set; }
    public string DataControllerXml { get; set; }
    public string CompanyName { get; set; }
    public string Version { get; set; }
    public int CoontrollerFkId { get; set; }
    public string FileName { get; set; }
  }
}
