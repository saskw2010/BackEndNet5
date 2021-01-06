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
  }
}
