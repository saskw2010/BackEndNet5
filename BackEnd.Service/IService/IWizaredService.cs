using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IWizaredService
  {
    Boolean createBackUp(string path, string fileName);
    Boolean createBackUpDataController(string path, string fileName);
    Boolean DeleteOldFile(string path, string fileName);
   Task<Result> insertControllers(xmlControllerViewModel xmlControllerVm);
   Task<Result> insertXmlFile(List<XmlFileViewModel> XmlFileVM);
   int chechVersionOfCompany(string fileName,string companyName);
   int validController(string fileName);
    Task<Result> InsertDataController(List<dataControllerViewModel> dataControllerVM);
  }
}
