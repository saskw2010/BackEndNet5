using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using BackEnd.DAL.Entities;

namespace RealState.DAL.IBackEndContext

{
  public interface IBackEndContext
  {
    DbSet<WorkSpace> WorkSpaces { get; set; }
    DbSet<UserType> UserType { get; set; }
    DbSet<AspNetUsersTypes> AspNetUsersTypes { get; set; }
    DbSet<AspNetusertypjoin> AspNetusertypjoin { get; set; }
    DbSet<AspNetUsersTypes_roles> AspNetUsersTypes_roles { get; set; }
    DbSet<EsSrClient> EsSrClient { get; set; }
    DbSet<EsSrTechnical> EsSrTechnical { get; set; }
    DbSet<FileManager> FileManager { get; set; }
    DbSet<FileManagerRole> FileManagerRole { get; set; }
    DbSet<fileManagerExtentions> fileManagerExtentions { get; set; }
    DbSet<EsSrItemTechnical> EsSrItemTechnical { get; set; }
    DbSet<EsSrWorkshopRegion> EsSrWorkshopRegion { get; set; }
    DbSet<EsSrAttache> EsSrAttache { get; set; }
    DbSet<EsSrPeriodLock> EsSrPeriodLock { get; set; }
    DbSet<EsSrPeriod> EsSrPeriod { get; set; }
    DbSet<EsSrPeriodTechnical> EsSrPeriodTechnical { get; set; }
    DbSet<EsSrOrder> EsSrOrder { get; set; }
    DbSet<EsSrOrderStage> EsSrOrderStage { get; set; }
    DbSet<EsSrOrderStageBase> EsSrOrderStageBase { get; set; }
    DbSet<EsSrOrderStageBaseCatgeory> EsSrOrderStageBaseCatgeory { get; set; }
    DbSet<EsSrTechnicalWorkDays> EsSrTechnicalWorkDays { get; set; }
    DbSet<EsSrWorkDay> EsSrWorkDay { get; set; }
    DbSet<EsSrEngineer> EsSrEngineer { get; set; }
    DbSet<EsSrSupervisor> EsSrSupervisor { get; set; }
    DbSet<EsSrWorkshop> EsSrWorkshop { get; set; }
    DbSet<xmlController> xmlController { get; set; }
    DbSet<XmlFile> XmlFile { get; set; }
    DbSet<dataController> dataController { get; set; }
    DbSet<dataController_commands> dataController_commands { get; set; }
    DbSet<dataController_commandstableslist> dataController_commandstableslist { get; set; }
    DbSet<dataController_fields_field> dataController_fields_field { get; set; }
    DbSet<dataController_views> dataController_views { get; set; }
    DbSet<dataController_dataFieldsGrid> dataController_dataFieldsGrid { get; set; }
    List<Object> GetEsSr_Proc_GetActiveItems();
  }
}
