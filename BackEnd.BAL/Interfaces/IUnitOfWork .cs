using BackEnd.BAL.Repository;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        int Save();

        GenericRepository<T> Repository<T>() where T : class, new();

        Task<int> SaveAsync();
       GenericRepository<WorkSpace> WorkSpaceRepository { get; }
       GenericRepository<AspNetUsersTypes> AspNetUsersTypesRepository { get; }
       GenericRepository<AspNetUsersTypes_roles> AspNetUsersTypes_rolesRepository { get; }
       GenericRepository<AspNetusertypjoin> AspNetusertypjoinRepository { get; }
       GenericRepository<EsSrClient> EsSrClientRepository { get; }
       GenericRepository<EsSrTechnical> EsSrTechnicalRepository { get; }
       GenericRepository<FileManager> FileManagerRepository { get; }
       GenericRepository<FileManagerRole> FileManagerRoleRepository { get; }
       GenericRepository<EsSrWorkshopRegion> EsSrWorkshopRegionRepository { get; }
       GenericRepository<EsSrItemTechnical> EsSrItemTechnicalRepository { get; }
       GenericRepository<EsSrAttache> EsSrAttacheRepository { get; }
       GenericRepository<EsSrPeriodLock> EsSrPeriodLockRepository { get; }
       GenericRepository<EsSrPeriod> EsSrPeriodRepository { get; }
       GenericRepository<EsSrPeriodTechnical> EsSrPeriodTechnicalRepository { get; }
       GenericRepository<EsSrOrder> EsSrOrderRepository { get; }
       GenericRepository<EsSrOrderStageBase> EsSrOrderStageBaseRepository { get; }
       GenericRepository<EsSrOrderStage> EsSrOrderStageRepository { get; }
       GenericRepository<EsSrOrderStageBaseCatgeory> EsSrOrderStageBaseCatgeoryRepository { get; }
       GenericRepository<EsSrTechnicalWorkDays> EsSrTechnicalWorkDayRepository { get; }
       GenericRepository<EsSrWorkDay> EsSrWorkDayRepository { get; }
       GenericRepository<EsSrSupervisor> EsSrSupervisorRepository { get; }
       GenericRepository<EsSrEngineer> EsSrEngineerRepository { get; }
       GenericRepository<xmlController> xmlControllerRepository { get; }
       GenericRepository<XmlFile> XmlFileRepository { get; }
       GenericRepository<dataController> dataControllersRepository { get; }
       GenericRepository<dataController_commands> dataController_commandsRepository { get; }
       GenericRepository<dataController_commandstableslist> dataController_commandstableslistRepository { get; }
       GenericRepository<dataController_fields_field> dataController_fields_fieldRepository { get; }
       GenericRepository<dataController_views> dataController_viewsRepository { get; }
       GenericRepository<dataControllerCollection> dataControllerCollectionRepository { get; }

  }
}
