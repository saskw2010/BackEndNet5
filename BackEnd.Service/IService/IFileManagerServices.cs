using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public interface IFileManagerServices
  {
    Task<Result> GetAllFileManagersByRolesName(List<string> RoleNames);
    Result GetAllFileManagersePathes();
    Result GetAllFileManagerPathes(int id);
    Task<Result> addFileManagerPathes(FileManagerViewModel FileManagerViewModel);
    Result delete(int id);
    Task<Result> UpdateFileManagerPathes(FileManagerViewModel fileManagerViewModel);
  }
}
