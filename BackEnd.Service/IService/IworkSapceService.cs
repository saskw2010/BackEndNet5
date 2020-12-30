using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IworkSapceService
  {
    Task<Result> CreateWorkspace(WorkSpaceVm workSpaceVm);
    Task<WorkSpaceVm> InsertWorkspace(WorkSpaceVm workSpace);
    Result pagginationFunction(string userId ,int pageNumber = 0, int pageSize = 0);
    Result pagginationFunctionWithFilter(string searchword, string userId, int pageNumber = 0, int pageSize = 0);
    Result CheckAvailability(string workSpaceName);
  }
}
