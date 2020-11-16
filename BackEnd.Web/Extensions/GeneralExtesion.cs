using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Extensions
{
  public static class GeneralExtesion
  {
    public static string getUserId(this HttpContext httpContext) {
      if (httpContext.User == null) {
        return string.Empty;
      }
      return httpContext.User.Claims.Single(x => x.Type == "id").Value;
    }
  }
}
