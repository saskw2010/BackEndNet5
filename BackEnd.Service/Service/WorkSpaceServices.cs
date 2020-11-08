using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.BAL.Repository;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class WorkSpaceServices : IworkSapceServices
  {
    private UnitOfWork unitOfWork;
    private IMapper _mapper;
    public WorkSpaceServices(IUnitOfWork unitOfWork,
      BakEndContext context,
      IMapper mapper
      )
    {
      this.unitOfWork = new UnitOfWork(context);
      _mapper = mapper;
    }
    public async Task<Result> CreateWorkspace(WorkSpaceVm workspace)
    {
      string domainName = workspace.WorkSpaceName;
      string appPoolName = "Classic .NET AppPool";
      string webFiles = "F:\\asd";
      if (IsWebsiteExists(domainName) == false)
      {
        ServerManager iisManager = new ServerManager();
        iisManager.Sites.Add(domainName, "http", "*:8080:", webFiles);
        iisManager.ApplicationDefaults.ApplicationPoolName = appPoolName;
        iisManager.CommitChanges();
        return new Result { success = true };
      }
      else
      {
        return new Result { success = true };
      }
    }


    

    public static bool IsWebsiteExists(string strWebsitename)
    {
      ServerManager serverMgr = new ServerManager();
      Boolean flagset = false;
      SiteCollection sitecollection = serverMgr.Sites;
      flagset = sitecollection.Any(x => x.Name == strWebsitename);
      return flagset;
    }

    public async Task<Result> InsertWorkspace(WorkSpaceVm workSpaceVm)
    {
      //WorkSpace workspace = new WorkSpace {
      //  UserName = workSpaceVm.UserName,
      //  WorkSpaceName= workSpaceVm.WorkSpaceName,
      //  DatabaseName= workSpaceVm.DatabaseName
      //};
      WorkSpace workspace = _mapper.Map<WorkSpace>(workSpaceVm);
      unitOfWork.WorkSpaceRepository.Insert(workspace);
      await  unitOfWork.SaveAsync();
      return new Result { success = true };
    }


    public Result pagginationFunction( int pageNumber = 1, int pageSize = 2)
    {
      // Get's No of Rows Count 
      int count = unitOfWork.WorkSpaceRepository.Get().Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = unitOfWork.WorkSpaceRepository.Get(page:pageNumber,Take:pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = items
      };

      // Setting Header
      // HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

      // Returing List of Customers Collections
     
      var res = new Result
      {
        success = true,
        Data = paginationMetadata,
        code = "200",
        message = null
      };
      return res;
    }

    //----------------------------------Eng Mostafa-----------------------
    public void r104Implementation(WorkSpaceVm workspace)
    {
      // This is the placeholder for method implementation.
      ServerManager serverManager = new ServerManager();
      Configuration config = serverManager.GetApplicationHostConfiguration();
      ConfigurationSection sitesSection = config.GetSection("system.applicationHost/sites");
      ConfigurationElementCollection sitesCollection = sitesSection.GetCollection();

      ConfigurationElement siteElement = sitesCollection.CreateElement("site");
      siteElement["name"] = workspace.WorkSpaceName;
      siteElement["id"] = 3;
      siteElement["serverAutoStart"] = true;

      ConfigurationElementCollection bindingsCollection = siteElement.GetCollection("bindings");
      ConfigurationElement bindingElement = bindingsCollection.CreateElement("binding");
      bindingElement["protocol"] = "http"; 
      bindingElement["bindingInformation"] = "*:80:www.contoso.com";
      bindingsCollection.Add(bindingElement);

      ConfigurationElementCollection siteCollection = siteElement.GetCollection();
      ConfigurationElement applicationElement = siteCollection.CreateElement("application");
      applicationElement["path"] = "/";
      ConfigurationElementCollection applicationCollection = applicationElement.GetCollection();
      ConfigurationElement virtualDirectoryElement = applicationCollection.CreateElement("virtualDirectory");
      virtualDirectoryElement["path"] = "/";
      virtualDirectoryElement["physicalPath"] = @"F:\work1";
      applicationCollection.Add(virtualDirectoryElement);
      siteCollection.Add(applicationElement);
      sitesCollection.Add(siteElement);

      serverManager.CommitChanges();
    }



    //----------------------------------End of Eng Mostafa----------------

  }
}
