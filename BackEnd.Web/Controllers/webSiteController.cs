using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using BackEnd.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  //[Authorize]
  public class webSiteController : Controller
  {
    private IworkSapceService _websiteServices;
    private IConfiguration configuration;
    private readonly BakEndContext Context;
    public webSiteController(IworkSapceService websiteServices, BakEndContext dbContext,
      IConfiguration iConfig)
    {
      _websiteServices = websiteServices;
      configuration = iConfig;
      Context = dbContext;
    }
    [HttpPost(ApiRoute.WebSite.CreateWebsite)]
    public async Task<Result> CreateWebsite([FromBody] WorkSpaceVm request)
    {
      //var res=await _websiteServices.CreateWorkspace(request);
      var workspacevm = await _websiteServices.InsertWorkspace(request);
      WorkspacelistBusinessRules wo = new WorkspacelistBusinessRules(configuration, Context);
      var res = wo.r100Implementation(workspacevm);
      return res;
    }


    [HttpPost(ApiRoute.WebSite.CreateWebsiteV2)]
    public Boolean CreateWebsiteV2(WorkSpaceVm workspace)
    {
      WorkspacelistBusinessRules wo = new WorkspacelistBusinessRules(configuration, Context);
      wo.r100Implementation(workspace);
      //_websiteServices.r104Implementation(workspace);
      return true;
    }

    [HttpGet(ApiRoute.WebSite.Get)]
    public Result Get(string userId, int pageNumber = 1, int pageSize = 2)
    {
      var res = _websiteServices.pagginationFunction(userId, pageNumber, pageSize);
      return res;
    }

    //begin::search
    [HttpGet(ApiRoute.WebSite.Filter)]
    public Result Filter(string searchword, string userId, int pageNumber = 1, int pageSize = 2)
    {
      var res = _websiteServices.pagginationFunctionWithFilter(searchword, userId, pageNumber, pageSize);
      return res;
    }
    //end::search

    #region CheckAvailability
    [HttpGet(ApiRoute.WebSite.CheckAvailability)]
    public Result CheckAvailability(string workSpaceName)
    {
      var res = _websiteServices.CheckAvailability(workSpaceName);
      return res;
    }
    #endregion

    [HttpGet("GetAllSites")]
    public Result GetAllSites()
    {
      List<string> NameOfSites = new List<string>();
      using (ServerManager serverManager = new ServerManager())
      {

        var sites = serverManager.Sites;
        foreach (var site in sites) {
          NameOfSites.Add(site.Name);
        }
       
      }
      return new Result { data = NameOfSites };
    }

  }
}
