using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Syncfusion.EJ2.FileManager.Base;
using BackEnd.BAL.Models;
using System.Threading.Tasks;
using BackEnd.Service.IService;

namespace EJ2APIServices.Controllers
{
  public class FileManagerController : Controller
  {
    private IFileManagerServices _fileManagerServices;
    public PhysicalFileProvider operation;
    public string basePath;
    static string root = "F:\\asd";
    public FileManagerController(
      IHostingEnvironment hostingEnvironment,
      IFileManagerServices fileManagerServices)
    {
      this.basePath = hostingEnvironment.ContentRootPath;
      this.operation = new PhysicalFileProvider();
      this.operation.RootFolder(FileManagerController.root);
      _fileManagerServices = fileManagerServices;
    }
    [HttpPost("api/FileManager/FileOperations")]
    public object FileOperations([FromBody] FileManagerDirectoryContent args)
    {
      if (args.Action == "delete" || args.Action == "rename")
      {
        if ((args.TargetPath == null) && (args.Path == ""))
        {
          FileManagerResponse response = new FileManagerResponse();
          response.Error = new ErrorDetails { Code = "401", Message = "Restricted to modify the root folder." };
          return this.operation.ToCamelCase(response);
        }
      }
      switch (args.Action)
      {
        case "read":
          // reads the file(s) or folder(s) from the given path.
          return this.operation.ToCamelCase(this.operation.GetFiles(args.Path, args.ShowHiddenItems));
        case "delete":
          // deletes the selected file(s) or folder(s) from the given path.
          return this.operation.ToCamelCase(this.operation.Delete(args.Path, args.Names));
        case "copy":
          // copies the selected file(s) or folder(s) from a path and then pastes them into a given target path.
          return this.operation.ToCamelCase(this.operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
        case "move":
          // cuts the selected file(s) or folder(s) from a path and then pastes them into a given target path.
          return this.operation.ToCamelCase(this.operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData));
        case "details":
          // gets the details of the selected file(s) or folder(s).
          return this.operation.ToCamelCase(this.operation.Details(args.Path, args.Names, args.Data));
        case "create":
          // creates a new folder in a given path.
          return this.operation.ToCamelCase(this.operation.Create(args.Path, args.Name));
        case "search":
          // gets the list of file(s) or folder(s) from a given path based on the searched key string.
          return this.operation.ToCamelCase(this.operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive));
        case "rename":
          // renames a file or folder.
          return this.operation.ToCamelCase(this.operation.Rename(args.Path, args.Name, args.NewName));
      }
      return null;
    }

   // uploads the file(s) into a specified path
     [HttpPost("api/FileManager/Upload")]
    public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action)
    {
      FileManagerResponse uploadResponse;
      uploadResponse = operation.Upload(path, uploadFiles, action, null);
      if (uploadResponse.Error != null)
      {
        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";
        Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
        Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
      }
      return Content("");
    }

    // downloads the selected file(s) and folder(s)
    [HttpPost("api/FileManager/Download")]
    public IActionResult Download(string downloadInput)
    {
      FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
      return operation.Download(args.Path, args.Names, args.Data);
    }

    // gets the image(s) from the given path
    [HttpGet("api/FileManager/GetImage")]
    public IActionResult GetImage(FileManagerDirectoryContent args)
    {
      return this.operation.GetImage(args.Path, args.Id, false, null, null);
    }

    #region GetAllFileManagersByRoleId
    [HttpPost("api/FileManager/GetAllFileManagersByRolesName")]
    public async Task<Result> GetAllFileManagersByRolesName([FromBody]List<string>RoleName) {
     return await _fileManagerServices.GetAllFileManagersByRolesName(RoleName);
    }
    #endregion

    #region changePathDirectory
    [HttpPost("api/FileManager/changePathDirectory")]
    public async Task<Result> changePathDirectory([FromBody] PathFileManager pathFileManager)
    {
      try
      {
        FileManagerController.root = pathFileManager.PathFile;
        return new Result {
          success = true,
          code="200"
        };
      }
      catch (Exception ex) {
        return new Result
        {
          success = true,
          code = "403"
        };
      }
      
    }
    #endregion


  }

}
