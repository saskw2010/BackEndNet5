using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
  public class TestController:Controller
  {
    [AllowAnonymous]
    [HttpGet("api/user")]
    public IActionResult Get() {
      return Ok(new {name="Mahmoud"});
    }

    [Authorize]
    [HttpGet("api/Auth")]
    public IActionResult Get2()
    {
      return Ok(new { name = "Mahmoud auth" });
    }
  }
}
