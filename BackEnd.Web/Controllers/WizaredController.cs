using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using BackEnd.Service.IService;

namespace BackEnd.Web.Controllers
{
  public class WizaredController : ControllerBase
  {
    private IWizaredService _WizaredService;
    public WizaredController(IWizaredService WizaredService)
    {
      _WizaredService = WizaredService;
    }
    #region ReadModelXml
    [HttpPost("ReadModelXml")]
    public void ReadModelXml()
    {
      string xmlns = "urn:schemas-codeontime-com:data-aquarium";
      var serializer = new XmlSerializer(typeof(DataModel), xmlns);
      DataModel resultingMessage = (DataModel)serializer.Deserialize(new XmlTextReader(@"E:\WytSky\CodeInTime\EsSrOrganisation.model.xml"));
      resultingMessage.Columns.Column.Add(new Column { Name = "mahmoud saad" });
      TextWriter txtWriter = new StreamWriter(@"E:\\WytSky\\CodeInTime\\EsSrOrganisation2.model.xml");
      var ns = new XmlSerializerNamespaces();
      ns.Add("", xmlns);
      serializer.Serialize(txtWriter, resultingMessage, ns);
      txtWriter.Close();
    }
    #endregion
    #region ReadControllerXml
    [HttpPost("ReadControllerXml")]
    public void ReadControllerXml()
    {
      string xmlns = "urn:schemas-codeontime-com:data-aquarium";
      var serializer = new XmlSerializer(typeof(DataController), xmlns);
      DataController resultingMessage = (DataController)serializer.Deserialize(new XmlTextReader(@"E:\WytSky\CodeInTime\EsSrOrganisation.xml"));
      TextWriter txtWriter = new StreamWriter(@"E:\\WytSky\\CodeInTime\\EsSrOrganisation2.xml");
      var ns = new XmlSerializerNamespaces();
      ns.Add("", xmlns);
      serializer.Serialize(txtWriter, resultingMessage, ns);
      txtWriter.Close();
    }
    #endregion
    [HttpGet("GetAllFinNameFromController")]
    #region GetAllFinNameFromController
    public Result GetAllFinNameFromController()
    {
      List<string> FileNames = new List<string>();
      string folderPath = @"E:\WytSky\CodeInTime\Jaber\controllers";
      foreach (string file in Directory.EnumerateFiles(folderPath, "*.model.xml"))
      {
        string fileName =Path.GetFileNameWithoutExtension(file);
        string[] FileBase = fileName.Split('.');
        FileNames.Add(FileBase[0]);
      }
      return new Result
      {
        success=true,
        code="200",
        data= FileNames
      };
    }
    #endregion


    #region GetXml
    [HttpGet("GetDataMode")]
    public Result GetDataMode(string NameOfModel)
    {
      try
      {
        string xmlns = "urn:schemas-codeontime-com:data-model";
        var serializer = new XmlSerializer(typeof(DataModel), xmlns);
        DataModel resultingMessage = (DataModel)serializer.Deserialize(new XmlTextReader(@"E:\WytSky\CodeInTime\Jaber\controllers\" + NameOfModel + ".model.xml"));
        return new Result { success = true, code = "200", data = resultingMessage };
      }
      catch (Exception ex)
      {
        return new Result { success = false, code = "403", data = ex.Message };
      }

    }
    #endregion
    [HttpPost("saveDataModel")]
    #region saveDataModel
    public Result saveDataModel([FromBody] SaveDataModel SaveDataModel) {
      //Create Backup
     var res= _WizaredService.createBackUp(@"E:\WytSky\CodeInTime\Jaber\controllers", SaveDataModel.controllerName);
      if (res == true) {
        var res2=_WizaredService.DeleteOldFile(@"E:\WytSky\CodeInTime\Jaber\controllers", SaveDataModel.controllerName+".model.xml");
        if (res2) {
          string xmlns = "urn:schemas-codeontime-com:data-model";
          var serializer = new XmlSerializer(typeof(DataModel), xmlns);
          TextWriter txtWriter = new StreamWriter(@"E:\WytSky\CodeInTime\Jaber\controllers\"+ SaveDataModel.controllerName + ".model.xml");
          var ns = new XmlSerializerNamespaces();
          ns.Add("", xmlns);
          serializer.Serialize(txtWriter, SaveDataModel.dataModel, ns);
          txtWriter.Close();
        }
      }
      //delete Old File
      return new Result { success= res };
    }
    #endregion


  }
}
