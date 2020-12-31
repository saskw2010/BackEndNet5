
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using BackEnd.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsSrAttacheController : Controller
    {

    private IEsSrAttacheService _EsSrAttacheService;
    private IidentityServices _identityServices;
    private readonly UserManager<ApplicationUser> _userManager;
    public EsSrAttacheController(
      IEsSrAttacheService EsSrAttacheService,
      IidentityServices identityServices,
      UserManager<ApplicationUser> userManager)
        {
           _EsSrAttacheService = EsSrAttacheService;
           _identityServices = identityServices;
           _userManager = userManager;
        }
        [HttpPost("FileUpload")]
        public async Task<Result> FileUpload(List<IFormFile> files,long id,string userName)
        {
      try {
        if (files == null || files.Count == 0)
          return new Result {success=false,code="403",message="no file is selected"};
        long size = files.Sum(f => f.Length);
        var filePaths = new List<string>();
        foreach (var formFile in files)
        {
          if (formFile.Length > 0)
          {
            // full path to file in temp location
            var folderName = Path.Combine("wwwroot/UploadFiles");
            if (!Directory.Exists(folderName))
            {
              Directory.CreateDirectory(folderName);
            }

            string extension = Path.GetExtension(formFile.FileName);


            string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            var fileNameWithDate = fileName + DateTime.Now.Ticks;
            var filepath = Path.Combine(folderName, fileNameWithDate + extension);


            using (var stream = new FileStream(filepath, FileMode.Create))
            {
              await formFile.CopyToAsync(stream);
            }
            filePaths.Add(fileNameWithDate + extension);
          }
        }
        // process uploaded files
        // Don't rely on or trust the FileName property without validation.
        //return Ok(new { count = files.Count, size, filePaths });
        
          return await _EsSrAttacheService.saveAttatche(filePaths, id, userName, size);
        
      } catch (Exception ex) {
        return new Result {
          success=false,
          message=ex.Message,
          code="403"
        };
      }

      

    }
    }
}
