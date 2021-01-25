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

    public async Task<Result> insertControllers(List<xmlControllerViewModel> xmlControllerVm)
    {
      try
      {
        List<xmlController> xmlController = new List<xmlController>();
        var Dto1 = _mapper.Map(xmlControllerVm, xmlController);
        _unitOfWork.xmlControllerRepository.AddRange(Dto1);
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
  }
}
