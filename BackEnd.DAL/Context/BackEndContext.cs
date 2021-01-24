using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.DAL.IBackEndContext;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BackEnd.DAL.Context
{
  public class BakEndContext : IdentityDbContext<ApplicationUser>, IBackEndContext
  {

    public BakEndContext(DbContextOptions<BakEndContext> options) : base(options)
    {
      
    }
    public virtual DbSet<WorkSpace> WorkSpaces { get; set; }
    public virtual DbSet<UserType> UserType { get; set; }
    public virtual DbSet<AspNetUsersTypes> AspNetUsersTypes { get; set; }
    public virtual DbSet<AspNetusertypjoin> AspNetusertypjoin { get; set; }
    public virtual DbSet<AspNetUsersTypes_roles> AspNetUsersTypes_roles { get; set; }
    public virtual DbSet<EsSrClient> EsSrClient { get; set; }
    public virtual DbSet<EsSrTechnical> EsSrTechnical { get; set; }
    public virtual DbSet<FileManager> FileManager { get; set; }
    public virtual DbSet<FileManagerRole> FileManagerRole { get; set; }
    public virtual DbSet<fileManagerExtentions> fileManagerExtentions { get; set; }
    public virtual DbSet<EsSrItemTechnical> EsSrItemTechnical { get; set; }
    public virtual DbSet<EsSrWorkshopRegion> EsSrWorkshopRegion { get; set; }
    public virtual DbSet<EsSrAttache> EsSrAttache { get; set; }
    public virtual DbSet<EsSrPeriodLock> EsSrPeriodLock { get; set; }
    public virtual DbSet<EsSrPeriod> EsSrPeriod { get; set; }
    public virtual DbSet<EsSrPeriodTechnical> EsSrPeriodTechnical { get; set; }
    public virtual DbSet<EsSrOrder> EsSrOrder { get; set; }
    public virtual DbSet<EsSrOrderStage> EsSrOrderStage { get; set; }
    public virtual DbSet<EsSrOrderStageBase> EsSrOrderStageBase { get; set; }
    public virtual DbSet<EsSrOrderStageBaseCatgeory> EsSrOrderStageBaseCatgeory { get; set; }
    public virtual DbSet<EsSrTechnicalWorkDays> EsSrTechnicalWorkDays { get; set; }
    public virtual DbSet<EsSrEngineer> EsSrEngineer { get; set; }
    public virtual DbSet<EsSrSupervisor> EsSrSupervisor { get; set; }
    public virtual DbSet<EsSrWorkDay> EsSrWorkDay { get; set; }
    public virtual DbSet<EsSrWorkshop> EsSrWorkshop { get; set; }
    public virtual DbSet<xmlController> xmlController { get; set; }
    public virtual DbSet<XmlFile> XmlFile { get; set; }
    public virtual DbSet<dataController> dataControllers { get; set; }
    public virtual DbSet<dataController_commands> dataController_commands { get; set; }
    public virtual DbSet<dataController_commandstableslist> dataController_commandstableslist { get; set; }
    public virtual DbSet<dataController_fields_field> dataController_fields_field { get; set; }
    public virtual DbSet<dataController_views> dataController_views { get; set; }
    public virtual DbSet<dataControllerCollection> dataControllerCollections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<AspNetusertypjoin>()
         .HasKey(c => new { c.IdAspNetUsers, c.UsrTypID });

      modelBuilder.Entity<AspNetUsersTypes_roles>()
         .HasKey(c => new { c.IdAspNetRoles, c.UsrTypID });
    }
   

  }
}
