using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IworkSapceServices
  {
    Task<Boolean> CreateWorkspace(WorkSpaceVm workSpaceVm);
    Task<Boolean> InsertWorkspace(WorkSpaceVm workSpace);
  }
}
