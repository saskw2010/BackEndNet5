using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IworkSapceServices
  {
    Task<Result> CreateWorkspace(WorkSpaceVm workSpaceVm);
    Task<WorkSpaceVm> InsertWorkspace(WorkSpaceVm workSpace);
    Result pagginationFunction(string userId ,int pageNumber = 0, int pageSize = 0);
    void r104Implementation(WorkSpaceVm workspace);
  }
}
