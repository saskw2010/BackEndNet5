using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.DAL.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.Web
{
    public class Program
    {
    //mahmoud
    public static async Task Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();
      using (var serviceScope = host.Services.CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<BakEndContext>();
        await dbContext.Database.MigrateAsync();

        //-------------------------add Rols in database when project start------------------
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
          var adminRole = new IdentityRole("Admin");
          await roleManager.CreateAsync(adminRole);
        }
        //----------------------------------------------------------------------------------
      }
      await host.RunAsync();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
