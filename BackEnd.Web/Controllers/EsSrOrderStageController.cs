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
  public class EsSrOrderStageController:ControllerBase
  {
    private readonly IEsSrOrderStageService _EsSrOrderStageService;
    public EsSrOrderStageController(IEsSrOrderStageService EsSrOrderStageService)
    {
      _EsSrOrderStageService = EsSrOrderStageService;
    }
    [HttpPost(ApiRoute.EsSrOrderStageRoute.UpdateEsSrOrderStage)]
    public async Task<Result> UpdateEsSrOrderStage([FromBody] EsSrOrderStageViewModel esSrOrderStageViewModel) {
      return await _EsSrOrderStageService.UpdateEsSrOrderStage(esSrOrderStageViewModel);
    }
  }
}
