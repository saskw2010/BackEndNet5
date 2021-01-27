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
using Microsoft.Extensions.Configuration;
using BackEnd.DAL.Context;
using System.Text;
using System.IO;

namespace EJ2APIServices.Controllers
{
  public class FileManagerController : Controller
  {
    private IFileManagerServices _fileManagerServices;
    public PhysicalFileProvider operation;
    public string basePath;
    //string root = "F:\\asd";
    public IConfiguration Configuration { get; }
    private readonly BakEndContext _BakEndContext;
    public FileManagerController(
      IHostingEnvironment hostingEnvironment,
      IFileManagerServices fileManagerServices,
      IConfiguration iConfig,
      BakEndContext BakEndContext)
    {
      this.basePath = hostingEnvironment.ContentRootPath;
      _BakEndContext = BakEndContext;
      this.operation = new PhysicalFileProvider(_BakEndContext);
      _fileManagerServices = fileManagerServices;
      Configuration = iConfig;
    }
    [HttpPost("api/FileManager/FileOperations")]
    public object FileOperations([FromBody] FileManagerDirectoryContent args, string pathFile)
    {
      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();
      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }

      BasePath = BasePath + "\\" + pathFile;
      this.operation.RootFolder(BasePath);
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
    public IActionResult Upload(string path, IList<IFormFile> uploadFiles, string action, string pathFile)
    {
      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();
      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else
      {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }
      BasePath = BasePath + "\\" + pathFile;
      this.operation.RootFolder(BasePath);
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
    public IActionResult Download(string downloadInput, string pathFile)
    {
      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();
      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else
      {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }
      BasePath = BasePath + "\\" + pathFile;
      this.operation.RootFolder(BasePath);
      FileManagerDirectoryContent args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
      return operation.Download(args.Path, args.Names, args.Data);
    }

    // gets the image(s) from the given path
    [HttpGet("api/FileManager/GetImage")]
    public IActionResult GetImage(FileManagerDirectoryContent args, string pathFile)

    {
      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();
      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else
      {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }
      BasePath = BasePath + "\\" + pathFile;
      return this.operation.GetImage(BasePath, args.Id, false, null, null);
    }

    //[HttpGet("api/FileManager/GetImage")]
    //public IActionResult GetImage(FileManagerDirectoryContent args)
    //{
    //  return this.operation.GetImage(args.Path, args.Id, false, null, null);
    //}

    #region GetAllFileManagersByRoleId
    [HttpPost("api/FileManager/GetAllFileManagersByRolesName")]
    public async Task<Result> GetAllFileManagersByRolesName([FromBody] List<string> RoleName)
    {
      return await _fileManagerServices.GetAllFileManagersByRolesName(RoleName);
    }
    #endregion

    #region Edit
    //Edit the selected file(s) 

    [HttpPost("EditeFileManager")]
    public Result EditeFileManager(string EditeName, string pathFile)
    {
      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();
      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else
      {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }

      string BasePathEdit = BasePath + "\\" + pathFile + "\\" + EditeName;
      //this.operation.RootFolder(BasePath);
      //StreamReader reader = new StreamReader(BasePathEdit, System.Text.Encoding.UTF8, true);
      string text = System.IO.File.ReadAllText(BasePathEdit, Encoding.UTF8);
      return new Result { data = text };
    }
    #endregion

    //savefile and backup the old file
    [HttpPost("SaveFileManager")]
    public Result SaveFileManager(string EditeName, string pathFile,
        string content)
    {

      string BasePath = "";
      var FileManagerConfiguration = Configuration
           .GetSection("FileManagerConfiguration")
           .Get<FileManagerConfiguration>();

      if (FileManagerConfiguration.staticPath != "")
      {
        BasePath = FileManagerConfiguration.staticPath;
      }
      else
      {
        BasePath = System.IO.Directory.GetCurrentDirectory();
      }
      string PathEdit = BasePath + "\\" + pathFile;
      string BackFileEdite = PathEdit + "\\" + EditeName;
      operation.createBackUp(PathEdit, EditeName);
      System.IO.File.Delete(BackFileEdite);
      using (StreamWriter outputFile = new StreamWriter(Path.Combine(BackFileEdite)))
      {

        outputFile.Write(content);

        outputFile.Close();
      }

      return new Result { success = true };
    }
  }

}
