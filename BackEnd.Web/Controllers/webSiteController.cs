using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using BackEnd.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class webSiteController: Controller
  {
    private IworkSapceServices _websiteServices;
    private IConfiguration configuration;
    public webSiteController(IworkSapceServices websiteServices,
      IConfiguration iConfig)
    {
      _websiteServices = websiteServices;
      configuration = iConfig;
    }
    [HttpPost(ApiRoute.WebSite.CreateWebsite)]
    public async Task<Result> CreateWebsite([FromBody] WorkSpaceVm request)
    {
      //var res=await _websiteServices.CreateWorkspace(request);
        var   workspacevm = await _websiteServices.InsertWorkspace(request);
        WorkspacelistBusinessRules wo = new WorkspacelistBusinessRules(configuration);
        var res=wo.r100Implementation(workspacevm);
        return res;
    }

    
    [HttpPost(ApiRoute.WebSite.CreateWebsiteV2)]
    public Boolean CreateWebsiteV2(WorkSpaceVm workspace)
    {
      WorkspacelistBusinessRules wo = new WorkspacelistBusinessRules(configuration);
      wo.r100Implementation(workspace);
      //_websiteServices.r104Implementation(workspace);
      return true;
    }

    [HttpGet(ApiRoute.WebSite.Get)]
    public Result Get(string userId,int pageNumber = 1, int pageSize = 2) {
    var res=_websiteServices.pagginationFunction(userId, pageNumber, pageSize);
     return res;
    }

  }
}
