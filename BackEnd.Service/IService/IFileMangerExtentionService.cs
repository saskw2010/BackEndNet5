using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IFileMangerExtentionService
    {
        Result GetAllFileManagersextention();
        Result GetAllFileManagersextention(int id);
        Task<Result> addFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel); 
        Task<Result> UpdateFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel);
        Result delete(int id);
    }
}
