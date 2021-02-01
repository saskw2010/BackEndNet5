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
