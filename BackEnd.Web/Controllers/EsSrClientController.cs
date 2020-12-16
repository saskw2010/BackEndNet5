using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class EsSrClientController: ControllerBase
  {
    private IEsSrClientService _EsSrClientService;
    public EsSrClientController(IEsSrClientService EsSrClientService)
    {
      _EsSrClientService = EsSrClientService;
    }

    [HttpPost(ApiRoute.Client.CreateCLient)]
    public async Task<Result> CreateCLient([FromBody] EsSrClientViewModel request)
    {
      var res = await _EsSrClientService.CreateCLient(request);
      return res;
    }
  }
}
