using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class WizaredService : IWizaredService
  {
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public WizaredService(
      IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    #region createBackUp
    public Boolean createBackUp(string path, string fileName)
    {
      try
      {
        string sourceDirectory = path;
        string destinationDirectory = System.IO.Path.Combine(sourceDirectory, "BackUp");
        if (!System.IO.Directory.Exists(destinationDirectory))
          System.IO.Directory.CreateDirectory(destinationDirectory);
        string extension = ".model.xml";
        var fileNameWithDate = fileName + DateTime.Now.Ticks;
        var fileNameWIthExtenation = fileName + extension;
        var fileVersionName = fileNameWithDate + extension;
        File.Copy(Path.Combine(sourceDirectory, fileNameWIthExtenation), Path.Combine(destinationDirectory, fileVersionName));
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }

    }
    #endregion
    #region createBackUp
    public Boolean createBackUpDataController(string path, string fileName)
    {
      try
      {
        string sourceDirectory = path;
        string destinationDirectory = System.IO.Path.Combine(sourceDirectory, "BackUp");
        if (!System.IO.Directory.Exists(destinationDirectory))
          System.IO.Directory.CreateDirectory(destinationDirectory);
        string extension = ".xml";
        var fileNameWithDate = fileName + DateTime.Now.Ticks;
        var fileNameWIthExtenation = fileName + extension;
        var fileVersionName = fileNameWithDate + extension;
        File.Copy(Path.Combine(sourceDirectory, fileNameWIthExtenation), Path.Combine(destinationDirectory, fileVersionName));
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }

    }
    #endregion
    #region DeleteOldModel
    public bool DeleteOldFile(string path, string fileName)
    {
      try
      {
        if (File.Exists(Path.Combine(path, fileName)))
        {
          // If file found, delete it    
          File.Delete(Path.Combine(path, fileName));
          return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }

    }
    #endregion

    #region insertControllers

    public async Task<Result> insertControllers(xmlControllerViewModel xmlControllerVm)
    {
      try
      {
        xmlController xmlController = new xmlController();
        var Dto1 = _mapper.Map(xmlControllerVm, xmlController);
        _unitOfWork.xmlControllerRepository.Insert(Dto1);
        var result = await _unitOfWork.SaveAsync();
        if (result == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            data = Dto1,
            message = "controllers saved successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "400",
            data = null,
            message = "controllers saved Faild"
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };

      }
    }
    #endregion

    #region insertXmlFile
    public async Task<Result> insertXmlFile(List<XmlFileViewModel> XmlFileVM)
    {
      try
      {
        List<XmlFile> XmlFileList = new List<XmlFile>();
        var Dto1 = _mapper.Map(XmlFileVM, XmlFileList);
        _unitOfWork.XmlFileRepository.AddRange(Dto1);
        var result = await _unitOfWork.SaveAsync();
        if (result == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            data = Dto1,
            message = "xmlFile saved successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "400",
            data = null,
            message = "xmlFile saved Faild"
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };

      }
    }
    #endregion

    #region chechVersionOfCompany
    public int chechVersionOfCompany(string fileName,string companyName)
    {
     return _unitOfWork.XmlFileRepository.Get(filter:(x=>x.CompanyName == companyName && x.FileName == fileName)).Count();
    }
    #endregion

    #region validController
    public int validController(string fileName)
    {
      return _unitOfWork.xmlControllerRepository.Get(x => x.dataControllerCollection_Name == fileName).Count();
    }
    #endregion

    public async Task<Result> InsertDataController(List<dataControllerViewModel> dataControllerVM)
    {
      try
      {
        List<dataController> dataControllerList = new List<dataController>();
        foreach (var item in dataControllerVM)
        {
          dataController dataController = new dataController();
          dataController.dataController_name = item.dataController_name;
          dataController.dataController_nativeSchema = item.dataController_nativeSchema;
          dataController.dataController_nativeTableName = item.dataController_nativeTableName;
          dataController.dataController_conflictDetection = item.dataController_conflictDetection;
          dataController.dataController_label = item.dataController_label;
          dataController.xmlFkId = item.xmlFkId;
          dataController.dataController_commandstableslist = new List<dataController_commandstableslist>();
          foreach (var obj in item.dataController_commandstableslist)
          {
            var dataController_commandstableslis = new dataController_commandstableslist();
            dataController_commandstableslis.dataController_commands_command_tableslist = obj.dataController_commands_command_tableslist;
            dataController_commandstableslis.dataController_name = obj.dataController_name;

            dataController.dataController_commandstableslist.Add(dataController_commandstableslis);
          }
          dataController.dataController_commands = new List<dataController_commands>();
          foreach (var obj in item.dataController_commands)
          {
            var datacontrollerCommand = new dataController_commands();
            datacontrollerCommand.dataController_name = obj.dataController_name;
            datacontrollerCommand.dataController_commands_command_type = obj.dataController_commands_command_type;
            datacontrollerCommand.dataController_commands_command_id = obj.dataController_commands_command_id;
            datacontrollerCommand.dataController_commands_command_text = obj.dataController_commands_command_text;
            datacontrollerCommand.dataController_commands_command_event = obj.dataController_commands_command_event;
            datacontrollerCommand.dataController_commands_command_output_fieldOutput_fieldName = obj.dataController_commands_command_output_fieldOutput_fieldName;
            dataController.dataController_commands.Add(datacontrollerCommand);
          }
          dataControllerList.Add(dataController);

          dataController.dataController_views = new List<dataController_views>();
          foreach (var obj in item.dataController_views)
          {
            var datacontrollerView = new dataController_views();
            datacontrollerView.dataController_name = obj.dataController_name;
            datacontrollerView.dataController_views_view_id = obj.dataController_views_view_id;
            datacontrollerView.dataController_views_view_type = obj.dataController_views_view_type;
            datacontrollerView.dataController_views_view_label = obj.dataController_views_view_label;
            datacontrollerView.dataController_views_view_commandId = obj.dataController_views_view_commandId;
            datacontrollerView.dataController_views_view_headerText = obj.dataController_views_view_headerText;
            List<dataController_dataFieldsGrid> dataFiledList = new List<dataController_dataFieldsGrid>();
            if (obj.dataController_dataFields != null) {
              foreach (var element in obj.dataController_dataFields)
              {
                dataController_dataFieldsGrid dataController_dataFieldsObj = new dataController_dataFieldsGrid();
                dataController_dataFieldsObj.FieldName = element.FieldName;
                dataController_dataFieldsObj.AliasFieldName = element.AliasFieldName;
                dataController_dataFieldsObj.Columns = element.Columns;
                dataFiledList.Add(dataController_dataFieldsObj);
              }
            }
            datacontrollerView.dataController_dataFields = dataFiledList;
            List<dataController_categoryCreate> categoryCreate = new List<dataController_categoryCreate>();
            List<dataController_categoryEdit> categoryEdit = new List<dataController_categoryEdit>();
            if (obj.dataController_categoryCreate.Count() > 0)
              {
              List<dataController_dataFieldCreate> dataController_dataFieldCreatList = new List<dataController_dataFieldCreate>();
              foreach (var categoryCreateVm in obj.dataController_categoryCreate)
                {
                foreach (var catDataField in categoryCreateVm.dataController_dataFieldCreate) {
                  dataController_dataFieldCreate dataController_dataFieldCreat = new dataController_dataFieldCreate();
                  dataController_dataFieldCreat.FieldName = catDataField.FieldName;
                  dataController_dataFieldCreat.AliasFieldName = catDataField.AliasFieldName;
                  dataController_dataFieldCreat.Columns = catDataField.Columns;
                  dataController_dataFieldCreatList.Add(dataController_dataFieldCreat);
                }
                  dataController_categoryCreate dataControllerCreateObj = new dataController_categoryCreate();
                  dataControllerCreateObj.headerText = categoryCreateVm.headerText;
                  dataControllerCreateObj.flow = categoryCreateVm.flow;
                  dataControllerCreateObj.description = categoryCreateVm.description;
                  dataControllerCreateObj.id = categoryCreateVm.id;
                  dataControllerCreateObj.dataController_dataFieldCreate = dataController_dataFieldCreatList;
                   categoryCreate.Add(dataControllerCreateObj);
                }
              datacontrollerView.dataController_categoryCreate = categoryCreate;
            }
          
     

            //------------------------------------------------------------------------------
            if (obj.dataController_categoryEdit.Count() > 0)
            {
              List<dataController_dataFieldEdit> dataController_dataFieldEditList = new List<dataController_dataFieldEdit>();
              foreach (var categoryCreateVm in obj.dataController_categoryEdit)
              {
                foreach (var catDataField in categoryCreateVm.dataController_dataFieldEdit)
                {
                  dataController_dataFieldEdit dataController_dataFieldEdit = new dataController_dataFieldEdit();
                  dataController_dataFieldEdit.FieldName = catDataField.FieldName;
                  dataController_dataFieldEdit.AliasFieldName = catDataField.AliasFieldName;
                  dataController_dataFieldEdit.Columns = catDataField.Columns;
                  dataController_dataFieldEditList.Add(dataController_dataFieldEdit);
                }
                dataController_categoryEdit dataControllerEditObj = new dataController_categoryEdit();
                dataControllerEditObj.headerText = categoryCreateVm.headerText;
                dataControllerEditObj.flow = categoryCreateVm.flow;
                dataControllerEditObj.description = categoryCreateVm.description;
                dataControllerEditObj.id = categoryCreateVm.id;
                dataControllerEditObj.dataController_dataFieldEdit = dataController_dataFieldEditList;
                categoryEdit.Add(dataControllerEditObj);
              }
              datacontrollerView.dataController_categoryEdit = categoryEdit;
            }

           
            dataController.dataController_views.Add(datacontrollerView);
            //------------------------------------------------------------------------------

          }
          dataControllerList.Add(dataController);

          dataController.dataController_fields_field = new List<dataController_fields_field>();
          foreach (var obj in item.dataController_fields_field)
          {
            var field = new dataController_fields_field();
            field.dataController_name = obj.dataController_name;
            field.dataController_fields_field_isPrimaryKey = obj.dataController_fields_field_isPrimaryKey;
            field.dataController_fields_field_name = obj.dataController_fields_field_name;
            field.dataController_fields_field_label = obj.dataController_fields_field_label;
            field.dataController_fields_field_allowNulls = obj.dataController_fields_field_allowNulls;
            field.dataController_fields_field_readOnly = obj.dataController_fields_field_readOnly;
            field.dataController_fields_field_type = obj.dataController_fields_field_type;
            field.dataController_fields_field_showInSummary = obj.dataController_fields_field_showInSummary;
            field.dataController_fields_field_dataFormatString = obj.dataController_fields_field_dataFormatString;
            field.dataController_fields_field_default = obj.dataController_fields_field_default;
            field.dataController_fields_field_length = obj.dataController_fields_field_length;
            field.dataController_fields_field_items_style = obj.dataController_fields_field_items_style;
            field.dataController_fields_field_items_dataTextField = obj.dataController_fields_field_items_dataTextField;
            field.dataController_fields_field_items_newDataView = obj.dataController_fields_field_items_newDataView;
            field.dataController_fields_field_items_dataValueField = obj.dataController_fields_field_items_dataValueField;
            field.dataController_fields_field_items_dataController = obj.dataController_fields_field_items_dataController;
            dataController.dataController_fields_field.Add(field);
          }
          dataControllerList.Add(dataController);
        }
        
        _unitOfWork.dataControllersRepository.AddRange(dataControllerList);
         var result = await _unitOfWork.SaveAsync();
        if (result == 200)
        {
          return new Result
          {
            success = true,
            code = "200",
            data = null,
            message = "controllers saved successfuly"
          };
        }
        else
        {
          return new Result
          {
            success = false,
            code = "400",
            data = null,
            message = "controllers saved Faild"
          };
        }
      }
      catch (Exception ex)
      {
        return new Result
        {
          success = false,
          code = "400",
          data = null,
          message = ExtensionMethods.FullMessage(ex)
        };

      }
    }


  }
}
