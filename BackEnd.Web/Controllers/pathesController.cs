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
    public class pathesController : ControllerBase
    {

       // private IFileManagerPathesService _IFileManagerPathesService;
        private IFileManagerServices _IFileManagerServices;

        public pathesController(IFileManagerServices IFileManagerServices)
        {
            _IFileManagerServices = IFileManagerServices;
        }
        //#region GetAllFileManagersByRoleId
        //[HttpPost("GetAllFileManagersByRolesName")]
        //public async Task<Result> GetAllFileManagersByRolesName([FromBody] List<string> RoleName)
        //{
        //    return await _IFileManagerServices.GetAllFileManagersByRolesName(RoleName);
        //}
        //#endregion
        [HttpPost("addFileManagerPathes")]
        public async Task<Result> addFileManagerPathes(FileManagerViewModel FileManagerViewModel)
        {
            return await _IFileManagerServices.addFileManagerPathes(FileManagerViewModel);
        }


        [HttpPut("UpdateFileManagerPathes")]
        public async Task<Result> UpdateFileManagerPathes(FileManagerViewModel FileManagerViewModel)
        {
            return await _IFileManagerServices.UpdateFileManagerPathes(FileManagerViewModel);
        }
        [HttpGet("GetAllFileManagerPathes")]
        public Result GetAllFileManagersePathes()
        {
            return _IFileManagerServices.GetAllFileManagersePathes();
        }
        [HttpGet("GetAllFileManagerPathesByID")]
        public Result GetAllFileManagerPathesById(int id)
        {
            return _IFileManagerServices.GetAllFileManagerPathes(id);
        }
        [HttpDelete("Delete")]
        public Result delete(int id)
        {
            return _IFileManagerServices.delete(id);
        }
    }
}
