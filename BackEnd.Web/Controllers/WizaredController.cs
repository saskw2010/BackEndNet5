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
        string fileName = Path.GetFileNameWithoutExtension(file);
        string[] FileBase = fileName.Split('.');
        FileNames.Add(FileBase[0]);
      }
      return new Result
      {
        success = true,
        code = "200",
        data = FileNames
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
        var file = new XmlTextReader(ControllerPath + "\\Jaber\\controllers\\" + NameOfModel + ".model.xml");
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
    public Result saveDataModel([FromBody] SaveDataModel SaveDataModel)
    {
      //Create Backup
      var wizaredConfiguration = Configuration
          .GetSection("WizaredConfiguration")
          .Get<WizaredConfiguration>();
      string ControllerPath = wizaredConfiguration.ControllerPath;

      var res = _WizaredService.createBackUp(ControllerPath, SaveDataModel.controllerName);
      if (res == true)
      {
        var res2 = _WizaredService.DeleteOldFile(ControllerPath, SaveDataModel.controllerName + ".model.xml");
        if (res2)
        {
          string xmlns = "urn:schemas-codeontime-com:data-model";
          var serializer = new XmlSerializer(typeof(DataModel), xmlns);
          TextWriter txtWriter = new StreamWriter(ControllerPath + SaveDataModel.controllerName + ".model.xml");
          var ns = new XmlSerializerNamespaces();
          ns.Add("", xmlns);
          serializer.Serialize(txtWriter, SaveDataModel.dataModel, ns);
          txtWriter.Close();
        }
        return new Result { success = true, code = "200", message = "DataModel Updated Successfuly" };
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
        var file = new XmlTextReader(ControllerPath + "\\Jaber\\controllers\\" + NameOfDataController + ".xml");
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
    public async Task<Result> Recurence(string companyName)
    {
      int countOfCompany = _WizaredService.validController(companyName);

      xmlControllerViewModel xmlControllerObj = new xmlControllerViewModel
      {
        dataControllerCollection_xmlns = "urn:schemas-codeontime-com:data-aquarium",
        dataControllerCollection_snapshot = "true",
        dataControllerCollection_Name = companyName,
        dataControllerCollection_Version = "V_" + countOfCompany

      };
      var res = await _WizaredService.insertControllers(xmlControllerObj);
      if (res.success == true)
      {
        var wizaredConfiguration = Configuration
       .GetSection("WizaredConfiguration")
       .Get<WizaredConfiguration>();
        //List <xmlControllerViewModel> xmlControllerList = new List<xmlControllerViewModel>();
        string folderPath = wizaredConfiguration.ControllerPath + "\\" + companyName + "\\controllers\\";

        List<string> nameOfFiles = new List<string>();
        foreach (string file in Directory.EnumerateFiles(folderPath, "*.model.xml"))
        {

          string fileName = Path.GetFileNameWithoutExtension(file);
          string[] FileBase = fileName.Split('.');
          nameOfFiles.Add(FileBase[0]);
        }

        if (res.success == true)
        {
          List<XmlFileViewModel> XmlFileListVM = new List<XmlFileViewModel>();
          foreach (var item in nameOfFiles)
          {
            string DataControllerText = System.IO.File.ReadAllText(wizaredConfiguration.ControllerPath + "\\" + companyName + "\\controllers\\" + item + ".xml", Encoding.UTF8);
            string DataModelText = System.IO.File.ReadAllText(wizaredConfiguration.ControllerPath + "\\" + companyName + "\\controllers\\" + item + ".model" + ".xml", Encoding.UTF8);

            XmlFileViewModel XmlFileVM = new XmlFileViewModel
            {
              DataControllerXml = DataControllerText,
              DataModelXml = DataModelText,
              CompanyName = companyName,
              CoontrollerFkId = ((xmlController)res.data).Id,
              FileName = item,
              Version = ((xmlController)res.data).dataControllerCollection_Version,

            };
            XmlFileListVM.Add(XmlFileVM);
          }

          var res2 = await _WizaredService.insertXmlFile(XmlFileListVM);

          if (res2.success == true)
          {
            var resDatacontroller = await snapShotDataControllerController((List<XmlFile>)res2.data);
            return resDatacontroller;
          }

        }


      }
      return res;




    }
    #endregion

    #region snapShot
    private async Task<Result> snapShotDataControllerController(List<XmlFile> XmlFileVM)
    {
      try
      {
        List<string> TextCommand = new List<string>();
        List<dataControllerViewModel> dataControllerViewModelList = new List<dataControllerViewModel>();
        foreach (var item in XmlFileVM)
        {
          string xmlns = "urn:schemas-codeontime-com:data-aquarium";
          var serializer = new XmlSerializer(typeof(DataController), xmlns);
          var datacontrollerObj = (DataController)serializer.Deserialize(new StringReader(item.DataControllerXml));
          var NameOfController = datacontrollerObj.Name;
          List<dataController_commandstableslistVM> dataControllerTableList = new List<dataController_commandstableslistVM>();
          foreach (var command in datacontrollerObj.Commands.Command)
          {
            dataControllerTableList.AddRange(getTableListTablesList(command.text, NameOfController));
          }
          //----map datacontrollerCommandList
          List<dataController_commandsViewModel> datacontrollerCommandList = new List<dataController_commandsViewModel>();
          foreach (var command in datacontrollerObj.Commands.Command)
          {
            var datacontrollerCommand = new dataController_commandsViewModel();

            datacontrollerCommand.dataController_name = NameOfController;
            datacontrollerCommand.dataController_commands_command_type = command.Type;
            datacontrollerCommand.dataController_commands_command_id = command.Id;
            datacontrollerCommand.dataController_commands_command_text = command.text;
            datacontrollerCommand.dataController_commands_command_event = command.Event;
            if (command.Output != null)
            {
              if (command.Output.FieldOutput != null)
              {
                datacontrollerCommand.dataController_commands_command_output_fieldOutput_fieldName = command.Output.FieldOutput.FirstOrDefault().FieldName;
              }
            }
            datacontrollerCommandList.Add(datacontrollerCommand);
          }
          //--end of datacontrollerCommandList
          //----map Fields
          List <dataController_fields_fieldViewModel> dataController_fieldsList = new List<dataController_fields_fieldViewModel>();
          foreach (var field in datacontrollerObj.Fields.Field)
          {
            var datacontrollerView = new dataController_fields_fieldViewModel();
            datacontrollerView.dataController_name = NameOfController;
            datacontrollerView.dataController_fields_field_isPrimaryKey = field.IsPrimaryKey;
            datacontrollerView.dataController_fields_field_name = field.Name;
            datacontrollerView.dataController_fields_field_label = field.Label;
            datacontrollerView.dataController_fields_field_allowNulls = field.AllowNulls;
            datacontrollerView.dataController_fields_field_readOnly = field.ReadOnly;
            datacontrollerView.dataController_fields_field_type = field.Type;
            datacontrollerView.dataController_fields_field_showInSummary = field.ShowInSummary;
            datacontrollerView.dataController_fields_field_dataFormatString = field.DataFormatString;
            datacontrollerView.dataController_fields_field_default = field.Default;
            if (field.Length != null) {
              datacontrollerView.dataController_fields_field_length = int.Parse(field.Length);
            }

            if (field.Items.Count() > 0)
            {
              datacontrollerView.dataController_fields_field_items_style = field.Items.FirstOrDefault().Style;
              datacontrollerView.dataController_fields_field_items_dataTextField = field.Items.FirstOrDefault().DataTextField;
              datacontrollerView.dataController_fields_field_items_newDataView = field.Items.FirstOrDefault().NewDataView;
              datacontrollerView.dataController_fields_field_items_dataValueField = field.Items.FirstOrDefault().DataValueField;
              datacontrollerView.dataController_fields_field_items_dataController = field.Items.FirstOrDefault().DataController;
            }
            dataController_fieldsList.Add(datacontrollerView);
          }
          //--end of Fields
          //----map view
          List<dataController_viewsViewModel> dataController_viewsList = new List<dataController_viewsViewModel>();
        
          foreach (var view in datacontrollerObj.Views.View)
          {
            List<dataController_categoryCreateVm> datacategoryCreateObjec = new List<dataController_categoryCreateVm>();
            List<dataController_categoryEditVm> datacategorEditList = new List<dataController_categoryEditVm>();

            var datacontrollerView = new dataController_viewsViewModel();
            datacontrollerView.dataController_name = NameOfController;
            datacontrollerView.dataController_views_view_id = view.Id;
            datacontrollerView.dataController_views_view_type = view.Type;
            datacontrollerView.dataController_views_view_label = view.Label;
            datacontrollerView.dataController_views_view_commandId = view.CommandId;
            datacontrollerView.dataController_views_view_headerText = view.HeaderText;
            List<dataController_dataFieldsGridViewModel> dataFiledList = new List<dataController_dataFieldsGridViewModel>();
            if (view.Id == "grid1")
            {
              foreach (var element in view.DataFields.DataField)
              {
                var dataFiledObjec = new dataController_dataFieldsGridViewModel();
                dataFiledObjec.FieldName = element.FieldName;
                dataFiledObjec.AliasFieldName = element.AliasFieldName;
                dataFiledObjec.Columns = element.Columns;
                dataFiledList.Add(dataFiledObjec);
              }
              datacontrollerView.dataController_dataFields = new List<dataController_dataFieldsGridViewModel>();
              datacontrollerView.dataController_dataFields.AddRange(dataFiledList);
             
            }
          
            if (view.Id == "createForm1")
            {
           
              List<dataController_dataFieldCreateVm> dataController_dataFieldCreateList = new List<dataController_dataFieldCreateVm>();
              foreach (var dataField in view.Categories.Category.DataFields.DataField) {
                var dataController_dataFieldCreateObj = new dataController_dataFieldCreateVm();
                dataController_dataFieldCreateObj.FieldName = dataField.FieldName;
                dataController_dataFieldCreateObj.AliasFieldName = dataField.AliasFieldName;
                dataController_dataFieldCreateObj.Columns = dataField.Columns;
                dataController_dataFieldCreateList.Add(dataController_dataFieldCreateObj);
              }

              var dataController_categoryCreateObjec = new dataController_categoryCreateVm();
              dataController_categoryCreateObjec.id = view.Categories.Category.Id;
              dataController_categoryCreateObjec.headerText = view.Categories.Category.HeaderText;
              dataController_categoryCreateObjec.flow = view.Categories.Category.Flow;
              dataController_categoryCreateObjec.description = view.Categories.Category.description;
              dataController_categoryCreateObjec.dataController_dataFieldCreate = dataController_dataFieldCreateList;
              datacategoryCreateObjec.Add(dataController_categoryCreateObjec);
            }

            if (view.Id == "editForm1")
            {

              List<dataController_dataFieldEditVm> dataController_dataFieldEditList = new List<dataController_dataFieldEditVm>();
              foreach (var dataField in view.Categories.Category.DataFields.DataField)
              {
                var dataController_dataFieldEditObj = new dataController_dataFieldEditVm();
                dataController_dataFieldEditObj.FieldName = dataField.FieldName;
                dataController_dataFieldEditObj.AliasFieldName = dataField.AliasFieldName;
                dataController_dataFieldEditObj.Columns = dataField.Columns;
                dataController_dataFieldEditList.Add(dataController_dataFieldEditObj);
              }

              var dataController_categoryEditObjec = new dataController_categoryEditVm();
              dataController_categoryEditObjec.id = view.Categories.Category.Id;
              dataController_categoryEditObjec.headerText = view.Categories.Category.HeaderText;
              dataController_categoryEditObjec.flow = view.Categories.Category.Flow;
              dataController_categoryEditObjec.description = view.Categories.Category.description;
              dataController_categoryEditObjec.dataController_dataFieldEdit = dataController_dataFieldEditList;
              datacategorEditList.Add(dataController_categoryEditObjec);
            }
            datacontrollerView.dataController_categoryCreate = new List<dataController_categoryCreateVm>();
            datacontrollerView.dataController_categoryCreate.AddRange(datacategoryCreateObjec);
            datacontrollerView.dataController_categoryEdit = new List<dataController_categoryEditVm>();
            datacontrollerView.dataController_categoryEdit.AddRange(datacategorEditList);
            dataController_viewsList.Add(datacontrollerView);
          }
          
          //--end of view

          dataControllerViewModel dataControllerVM = new dataControllerViewModel
          {
            dataController_name = datacontrollerObj.Name,
            dataController_nativeSchema = "dbo",
            dataController_nativeTableName = datacontrollerObj.Name,
            dataController_conflictDetection = datacontrollerObj.ConflictDetection,
            dataController_label = datacontrollerObj.Label,
            xmlFkId = item.Id,
            dataController_commandstableslist = dataControllerTableList,
            dataController_commands = datacontrollerCommandList,
            dataController_views = dataController_viewsList,
            dataController_fields_field = dataController_fieldsList


          };
          dataControllerViewModelList.Add(dataControllerVM);

        }
        return await _WizaredService.InsertDataController(dataControllerViewModelList);



      }
      catch (Exception ex)
      {
        return new Result { success = false,code="403", message = ExtensionMethods.FullMessage(ex) };
      }


    }
    #endregion

    #region
    public List<dataController_commandstableslistVM> getTableListTablesList(string iteratorcomamands_Commandtext1, string Name)
    {
      List<dataController_commandstableslistVM> dataControllerTableList = new List<dataController_commandstableslistVM>();
      SelectClauseDictionary expressions;
      var statementMatch = DataControllerBase.SqlSelectRegex1.Match(iteratorcomamands_Commandtext1);
      if (!statementMatch.Success)
        statementMatch = DataControllerBase.SqlSelectRegex2.Match(iteratorcomamands_Commandtext1);
      expressions = ParseSelectExpressions(statementMatch.Groups["Select"].Value);
      foreach (KeyValuePair<string, string> expres in expressions)
      {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        var expresvalue = expres.Value.Split(delimiterChars)[0];
        var mtable = expresvalue.Replace(@"\", string.Empty);
        if (mtable.Length > 0)
        {
          if (!dataControllerTableList.Any(x => x.dataController_commands_command_tableslist == mtable))
          {
            var dataControllerTableElement = new dataController_commandstableslistVM
            {
              dataController_name = Name,
              dataController_commands_command_tableslist = mtable,
            };
            dataControllerTableList.Add(dataControllerTableElement);
          }

        }

      }
      return dataControllerTableList;
    }
    #endregion
    private SelectClauseDictionary ParseSelectExpressions(string selectClause)
    {
      var expressions = new SelectClauseDictionary();
      var fieldMatch = DataControllerBase.SelectExpressionRegex.Match(selectClause);
      while (fieldMatch.Success)
      {
        var expression = fieldMatch.Groups["Expression"].Value;
        var fieldName = fieldMatch.Groups["FieldName"].Value;
        var aliasField = fieldMatch.Groups["Alias"].Value;
        if (!(string.IsNullOrEmpty(expression)))
        {
          if (string.IsNullOrEmpty(aliasField))
            if (string.IsNullOrEmpty(fieldName))
              aliasField = expression;
            else
              aliasField = fieldName;
          if (!(expressions.ContainsKey(aliasField)))
            expressions.Add(aliasField, expression);
        }
        fieldMatch = fieldMatch.NextMatch();
      }


      return expressions;

    }
  }
}
