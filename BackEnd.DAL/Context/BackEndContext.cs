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
    public virtual DbSet<dataController> dataController { get; set; }
    public virtual DbSet<dataController_commands> dataController_commands { get; set; }
    public virtual DbSet<dataController_commandstableslist> dataController_commandstableslist { get; set; }
    public virtual DbSet<dataController_fields_field> dataController_fields_field { get; set; }
    public virtual DbSet<dataController_views> dataController_views { get; set; }
    public DbSet<dataController_dataFieldsGrid> dataController_dataFieldsGrid { get; set; }
    public DbSet<dataController_categoryCreate> dataController_categoryCreate { get; set; }
    public DbSet<dataController_dataFieldCreate> dataController_dataFieldCreate { get; set; }
    public DbSet<dataController_categoryEdit> dataController_categoryEdit { get; set; }
    public DbSet<dataController_dataFieldEdit> dataController_dataFieldEdit { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<AspNetusertypjoin>()
         .HasKey(c => new { c.IdAspNetUsers, c.UsrTypID });

      modelBuilder.Entity<AspNetUsersTypes_roles>()
         .HasKey(c => new { c.IdAspNetRoles, c.UsrTypID });
    }

    public List<Object> GetEsSr_Proc_GetActiveItems() {
      List<object> list = new List<object>();
      using (var cmd = this.Database.GetDbConnection().CreateCommand())
      {
        cmd.CommandText = "EsSr_Proc_GetActiveItems";
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        this.Database.OpenConnection();
        using (var result = cmd.ExecuteReader())
        {
          if (result.HasRows)
          {
            while (result.Read())
            {
              list.Add(new
              {
                ItemID = result["ItemID"]?.ToString(),
                CatgeoryID = result["CatgeoryID"]?.ToString(),
                CatgeoryNameAr = result["CatgeoryNameAr"]?.ToString(),
                CatgeoryNameEn = result["CatgeoryNameEn"]?.ToString(),
                CurrencyID = result["CurrencyID"]?.ToString(),
                CurrencyNameAr = result["CurrencyNameAr"]?.ToString(),
                CurrencyNameEn = result["CurrencyNameEn"]?.ToString(),
                CatgeoryPicStockId = result["CatgeoryPicStockId"]?.ToString(),
                CatgeoryId1 = result["CatgeoryId1"]?.ToString(),
                CatgeoryParentId = result["CatgeoryParentId"]?.ToString(),
                CatgeoryHasSubCatgeory = result["CatgeoryHasSubCatgeory"]?.ToString(),
                CatgeoryDescriptionEn = result["CatgeoryDescriptionEn"]?.ToString(),
                CatgeoryDescriptionAr = result["CatgeoryDescriptionAr"]?.ToString(),
                CatgeoryShowOrder = result["CatgeoryShowOrder"]?.ToString(),
                CatgeoryDiscount = result["CatgeoryDiscount"]?.ToString(),
                CatgeoryIsActive = result["CatgeoryIsActive"]?.ToString(),
                CatgeoryIsDelete = result["CatgeoryIsDelete"]?.ToString(),
                CatgeoryPicStockControllername = result["CatgeoryPicStockControllername"]?.ToString(),
                CatgeoryPicStockPictureFileName = result["CatgeoryPicStockPictureFileName"]?.ToString(),
                CatgeoryImageUrl = result["CatgeoryImageUrl"]?.ToString(),
                PicStockID = result["PicStockID"]?.ToString(),
                PicStockControllername = result["PicStockControllername"]?.ToString(),
                PicStockPictureFileName = result["PicStockPictureFileName"]?.ToString(),
                OrganisationID = result["OrganisationID"]?.ToString(),
                OrganisationNameAr = result["OrganisationNameAr"]?.ToString(),
                OrganisationNameEn = result["OrganisationNameEn"]?.ToString(),
                NameAr = result["NameAr"]?.ToString(),
                NameEn = result["NameEn"]?.ToString(),
                DescriptionEn = result["DescriptionEn"]?.ToString(),
                DescriptionAr = result["DescriptionAr"]?.ToString(),
                ShowOrder = result["ShowOrder"]?.ToString(),
                Discount = result["Discount"]?.ToString(),
                CreatedHour = result["CreatedHour"]?.ToString(),
                EditHour = result["EditHour"]?.ToString(),
                IsActive = result["IsActive"]?.ToString(),
                Notes = result["Notes"]?.ToString(),
                CreatedBy = result["CreatedBy"]?.ToString(),
                CreatedOn = result["CreatedOn"]?.ToString(),
                ModifiedBy = result["ModifiedBy"]?.ToString(),
                ModifiedOn = result["ModifiedOn"]?.ToString(),
                IsDelete = result["IsDelete"]?.ToString(),
                Price = result["Price"]?.ToString(),
                IsPreview = result["IsPreview"]?.ToString(),
              });
            }
          
          }
        }
        return list;
      }
    }
  }
}
