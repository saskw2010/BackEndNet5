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
    private IUnitOfWork unitOfWork;
    private IMapper _mapper;
    public WorkSpaceServices(IUnitOfWork unitOfWork,
      IMapper mapper
      )
    {
      this.unitOfWork = unitOfWork;
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

    public async Task<WorkSpaceVm> InsertWorkspace(WorkSpaceVm workSpaceVm)
    {
      WorkSpace workspacerequest = _mapper.Map<WorkSpace>(workSpaceVm);
      unitOfWork.WorkSpaceRepository.Insert(workspacerequest);
      await unitOfWork.SaveAsync();
      WorkSpaceVm workspaceresponse = _mapper.Map<WorkSpaceVm>(workspacerequest);
      return workspaceresponse;
    }


    public Result pagginationFunction(string userId, int pageNumber = 1, int pageSize = 2)
    {
      // Get's No of Rows Count 
      int count = unitOfWork.WorkSpaceRepository.Get(filter: x => x.UserId == userId).Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = unitOfWork.WorkSpaceRepository.Get(filter: x => x.UserId == userId, page: pageNumber, Take: pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
      // map list of workspace to workspacevm
      var workspacevmList = _mapper.Map<List<WorkSpaceVm>>(items);
      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = workspacevmList
      };

      // Setting Header
      // HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

      // Returing List of Customers Collections

      var res = new Result
      {
        success = true,
        data = paginationMetadata,
        code = "200",
        message = null
      };
      return res;
    }

    //----------------------------------Eng Mostafa-----------------------

    public Result pagginationFunctionWithFilter(string searchword, string userId, int pageNumber = 0, int pageSize = 0)
    {

      // Get's No of Rows Count 
      int count = unitOfWork.WorkSpaceRepository.Get(filter: x => (x.UserId == userId)
      &&
      (searchword != null?
      x.WorkSpaceName.Contains(searchword) || x.DatabaseName.Contains(searchword) || x.UserName.Contains(searchword)
      :true)
      ).Count();

      // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1
      int CurrentPage = pageNumber;

      // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
      int PageSize = pageSize;

      // Display TotalCount to Records to User
      int TotalCount = count;

      // Calculating Totalpage by Dividing (No of Records / Pagesize)
      int TotalPages = (int)Math.Ceiling(count / (double)PageSize);


      // Returns List of Customer after applying Paging 
      var items = unitOfWork.WorkSpaceRepository.Get(filter: x => (x.UserId == userId) &&
      (searchword != null ?
      x.WorkSpaceName.Contains(searchword) || x.DatabaseName.Contains(searchword) || x.UserName.Contains(searchword)
      : true)
      , page: pageNumber, Take: pageSize);

      // if CurrentPage is greater than 1 means it has previousPage
      var previousPage = CurrentPage > 1 ? "Yes" : "No";

      // if TotalPages is greater than CurrentPage means it has nextPage
      var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
      // map list of workspace to workspacevm
      var workspacevmList = _mapper.Map<List<WorkSpaceVm>>(items);
      // Object which we are going to send in header 
      paginationMetadata paginationMetadata = new paginationMetadata
      {
        totalCount = TotalCount,
        pageSize = PageSize,
        currentPage = CurrentPage,
        nextPage = nextPage,
        previousPage = previousPage,
        data = workspacevmList
      };

      // Setting Header
      // HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

      // Returing List of Customers Collections

      var res = new Result
      {
        success = true,
        data = paginationMetadata,
        code = "200",
        message = null
      };
      return res;
    }

    #region CheckAvailability
    public Result CheckAvailability(string workSpaceName)
    {
      var res=unitOfWork.WorkSpaceRepository.GetEntity(filter:(x=>x.WorkSpaceName == workSpaceName));
      if (res != null)
      {
        return new Result
        {
          success = false,
          code = "403",
          message = "workspace not Avaiabel"
        };
      }
      else {
        return new Result
        {
          success = true,
          code = "200",
          message = "workspace Is Availabel"
        };
      }
    }
    #endregion

  }
}
