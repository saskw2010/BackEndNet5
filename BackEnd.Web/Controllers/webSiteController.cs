using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class webSiteController: Controller
  {
    private IworkSapceServices _websiteServices;
    public webSiteController(IworkSapceServices websiteServices)
    {
      _websiteServices = websiteServices;
    }
    [HttpPost(ApiRoute.WebSite.CreateWebsite)]
    public async Task<IActionResult> CreateWebsite([FromBody] WorkSpaceVm request)
    {
     //var res=await _websiteServices.CreateWorkspace(request);
        var   res = await _websiteServices.InsertWorkspace(request);
      return Ok(new Result {success= res });
    }

  }
}
