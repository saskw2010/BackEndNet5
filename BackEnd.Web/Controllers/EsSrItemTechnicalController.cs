using BackEnd.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class EsSrItemTechnicalController: ControllerBase
  {
    private IEsSrItemTechnicalService _EsSrItemTechnicalService;
    public EsSrItemTechnicalController(IEsSrItemTechnicalService EsSrItemTechnicalService)
    {
      _EsSrItemTechnicalService = EsSrItemTechnicalService;
    }
  }
}
