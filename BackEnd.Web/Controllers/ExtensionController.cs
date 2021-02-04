using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private IFileMangerExtentionService _IFileMangerExtentionService;
        public ExtensionController(IFileMangerExtentionService IFileMangerExtentionService)
        {
            _IFileMangerExtentionService = IFileMangerExtentionService;
        }
        [HttpPost("AddFileManagersByextention")]
        public async Task<Result> addFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel)
        {
            return await _IFileMangerExtentionService.addFileManagersByextention(fileMangerExtentionViewModel);
        }
        [HttpPut("UpdateFileManagersByextention")]
        public async Task<Result> UpdateFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel)
        {
            return await _IFileMangerExtentionService.UpdateFileManagersByextention(fileMangerExtentionViewModel);
        }
        [HttpGet("GetAllFileManagersextention")]
        public Result GetAllFileManagersextention()
        {
            return  _IFileMangerExtentionService.GetAllFileManagersextention();
        }
        [HttpGet("GetAllFileManagersextentionById")]
        public Result GetAllFileManagersextentionById(int id)
        {
            return _IFileMangerExtentionService.GetAllFileManagersextention(id);
        }
        [HttpDelete("Delete")]
        public Result delete(int id)
        {
            return _IFileMangerExtentionService.delete(id);
        }
    }
}
