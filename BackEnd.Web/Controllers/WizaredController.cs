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
using Microsoft.Extensions.Configuration;
using BackEnd.DAL.Entities;
using System.Text;

namespace BackEnd.Web.Controllers
{
  public class WizaredController : ControllerBase
  {
    private IWizaredService _WizaredService;
    public IConfiguration Configuration { get; }
    public WizaredController(IWizaredService WizaredService, IConfiguration iConfig)
    {
      _WizaredService = WizaredService;
      Configuration = iConfig;
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

    #region GetAllFinNameFromController
    [HttpGet("GetAllFinNameFromController")]
    public Result GetAllFinNameFromController()
    {
      var wizaredConfiguration = Configuration
         .GetSection("WizaredConfiguration")
         .Get<WizaredConfiguration>();
      List<string> FileNames = new List<string>();
      string folderPath = wizaredConfiguration.ControllerPath + "\\" + "Jaber\\controllers\\";
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
        var wizaredConfiguration = Configuration
          .GetSection("WizaredConfiguration")
          .Get<WizaredConfiguration>();
        string ControllerPath = wizaredConfiguration.ControllerPath;
        string xmlns = "urn:schemas-codeontime-com:data-model";
        var serializer = new XmlSerializer(typeof(DataModel), xmlns);
        var file = new XmlTextReader(ControllerPath+ "\\Jaber\\controllers\\" + NameOfModel + ".model.xml");
        DataModel resultingMessage = (DataModel)serializer.Deserialize(file);
        file.Close();
        return new Result { success = true, code = "200", data = resultingMessage };
      }
      catch (Exception ex)
      {
        return new Result { success = false, code = "403", data = ex.Message };
      }

    }
    #endregion

    #region saveDataModel
    [HttpPost("saveDataModel")]
    public Result saveDataModel([FromBody] SaveDataModel SaveDataModel) {
      //Create Backup
      var wizaredConfiguration = Configuration
          .GetSection("WizaredConfiguration")
          .Get<WizaredConfiguration>();
      string ControllerPath = wizaredConfiguration.ControllerPath;

      var res= _WizaredService.createBackUp(ControllerPath, SaveDataModel.controllerName);
      if (res == true) {
        var res2=_WizaredService.DeleteOldFile(ControllerPath, SaveDataModel.controllerName+".model.xml");
        if (res2) {
          string xmlns = "urn:schemas-codeontime-com:data-model";
          var serializer = new XmlSerializer(typeof(DataModel), xmlns);
          TextWriter txtWriter = new StreamWriter(ControllerPath + SaveDataModel.controllerName + ".model.xml");
          var ns = new XmlSerializerNamespaces();
          ns.Add("", xmlns);
          serializer.Serialize(txtWriter, SaveDataModel.dataModel, ns);
          txtWriter.Close();
        }
        return new Result { success = true,code="200",message="DataModel Updated Successfuly" };
      }
      return new Result { success = true, code = "403", message = "DataModel Updated Faild" };
    }
    #endregion

    #region GetDataController
    [HttpGet("GetDataController")]
    public Result GetDataController(string NameOfDataController)
    {
      try
      {
        var wizaredConfiguration = Configuration
          .GetSection("WizaredConfiguration")
          .Get<WizaredConfiguration>();
        string ControllerPath = wizaredConfiguration.ControllerPath;
        string xmlns = "urn:schemas-codeontime-com:data-aquarium";
        var serializer = new XmlSerializer(typeof(DataController), xmlns);
        var file = new XmlTextReader(ControllerPath +"\\Jaber\\controllers\\"+NameOfDataController + ".xml");
        DataController resultingMessage = (DataController)serializer.Deserialize(file);
        file.Close();
      return new Result { success = true, code = "200", data = resultingMessage };
    }
      catch (Exception ex)
      {
        return null;
        return new Result { success = false, code = "403", data = ex.Message };
      }

}
    #endregion


    #region saveDataController
    [HttpPost("saveDataController")]
    public Result saveDataController([FromBody] SaveDataController saveDataController)
    {
      //Create Backup
      var wizaredConfiguration = Configuration
          .GetSection("WizaredConfiguration")
          .Get<WizaredConfiguration>();
      string ControllerPath = wizaredConfiguration.ControllerPath;

      var res = _WizaredService.createBackUpDataController(ControllerPath, saveDataController.controllerName);
      if (res == true)
      {
        var res2 = _WizaredService.DeleteOldFile(ControllerPath, saveDataController.controllerName + ".xml");
        if (res2)
        {
          string xmlns = "urn:schemas-codeontime-com:data-aquarium";
          var serializer = new XmlSerializer(typeof(DataController), xmlns);
          TextWriter txtWriter = new StreamWriter(ControllerPath + saveDataController.controllerName + ".xml");
          var ns = new XmlSerializerNamespaces();
          ns.Add("", xmlns);
          serializer.Serialize(txtWriter, saveDataController.dataController, ns);
          txtWriter.Close();
        }
        return new Result { success = true, code = "200", message = "DataModel Updated Successfuly" };
      }
      return new Result { success = true, code = "403", message = "DataModel Updated Faild" };
    }
    #endregion


    #region Recurence
    [HttpGet("Recurence ")]
    public async Task<Result> Recurence(string controllerName)
    {
      var wizaredConfiguration = Configuration
         .GetSection("WizaredConfiguration")
         .Get<WizaredConfiguration>();
      List <xmlControllerViewModel> xmlControllerList = new List<xmlControllerViewModel>();
      string folderPath = wizaredConfiguration.ControllerPath+"\\"+ controllerName + "\\controllers\\";
      foreach (string file in Directory.EnumerateFiles(folderPath, "*.model.xml"))
      {
       
        string fileName = Path.GetFileNameWithoutExtension(file);
        string[] FileBase = fileName.Split('.');
        xmlControllerViewModel xmlControllerObj = new xmlControllerViewModel
        {
          Name= FileBase[0]
        };
        int countOfController = _WizaredService.validController(xmlControllerObj.Name);

        if (countOfController < 1) 
        xmlControllerList.Add(xmlControllerObj);

      }
      var res= await _WizaredService.insertControllers(xmlControllerList);

      List<XmlFileViewModel> XmlFileListVM = new List<XmlFileViewModel>();
      if (res.success == true) {
        foreach (var item in (List<xmlController>)res.data) {
          string DataControllerText = System.IO.File.ReadAllText(wizaredConfiguration.ControllerPath + "\\" + controllerName + "\\controllers\\" + item.Name + ".xml", Encoding.UTF8);
          string DataModelText = System.IO.File.ReadAllText(wizaredConfiguration.ControllerPath + "\\" + controllerName + "\\controllers\\" + item.Name+".model" + ".xml", Encoding.UTF8);
          var count=_WizaredService.chechVersionOfCompany(item.Name, controllerName);
          XmlFileViewModel XmlFileVM = new XmlFileViewModel{
            DataControllerXml= DataControllerText,
            DataModelXml= DataModelText,
            CompanyName= controllerName,
            CountryFkId= item.Id,
            FileName=item.Name,
            Version="V_"+ count
          };
          XmlFileListVM.Add(XmlFileVM);
          //  string xmlns = "urn:schemas-codeontime-com:data-aquarium";
          //  var serializer = new XmlSerializer(typeof(DataController), xmlns);
          //  var file = new XmlTextReader(wizaredConfiguration.ControllerPath + item.Name + ".xml");
          //  DataController resultingMessage = (DataController)serializer.Deserialize(file);
        }
   
      }
      var res2 = await _WizaredService.insertXmlFile(XmlFileListVM);
      return res2;
    }
    #endregion

  }
}
