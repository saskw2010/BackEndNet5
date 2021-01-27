using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BakEndContext Context;


        public UnitOfWork(BakEndContext dbContext)
        {
            Context = dbContext;
        }

        public GenericRepository<T> Repository<T>() where T : class, new()
        {
            return new GenericRepository<T>(Context);
        }


        public virtual int Save()
        {
            int returnValue = 200;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
                //  {
                try
                {
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        var number = sqlException.Number;

                        if (number == 547)
                        {
                            returnValue = 501;

                        }
                        else
                            returnValue = 500;
                    }
                }
                catch (Exception ex)
                {
                    //Log Exception Handling message                      
                    returnValue = 500;
                    dbContextTransaction.Rollback();
                }
            //    }

            return returnValue;
        }

        public virtual async Task<int> SaveAsync()
        {
            int returnValue = 200;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
        //try
        //{
          await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();
     //  }
        //        catch (DbUpdateException ex)
        //        {
        //            var sqlException = ex.GetBaseException() as SqlException;

        //            if (sqlException != null)
        //            {
        //                var number = sqlException.Number;

        //                if (number == 547)
        //                {
        //                    returnValue = 501;

        //                }
        //                else
        //                    returnValue = 500;
        //            }
        //           returnValue = 500;
        //}
        //catch (Exception)
        //{
        //  //Log Exception Handling message                      
        //  returnValue = 500;
        //  dbContextTransaction.Rollback();
        //}
      }

            return returnValue;
        }

        private bool disposed = false;



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    //begin workspace
    public GenericRepository<WorkSpace> WorkSpaceRepository
    {
      get
      {
        return new GenericRepository<WorkSpace>(Context);
      }
    }

    public GenericRepository<AspNetUsersTypes> AspNetUsersTypesRepository
    {
      get
      {
        return new GenericRepository<AspNetUsersTypes>(Context);
      }
    }

    public GenericRepository<AspNetUsersTypes_roles> AspNetUsersTypes_rolesRepository
    {
      get
      {
        return new GenericRepository<AspNetUsersTypes_roles>(Context);
      }
    }

    public GenericRepository<AspNetusertypjoin> AspNetusertypjoinRepository
    {
      get
      {
        return new GenericRepository<AspNetusertypjoin>(Context);
      }
    }

    public GenericRepository<EsSrClient> EsSrClientRepository
    { get
      {
        return new GenericRepository<EsSrClient>(Context);
      }
    }

    public GenericRepository<EsSrTechnical> EsSrTechnicalRepository
        {
          get
          {
            return new GenericRepository<EsSrTechnical>(Context);
          }
        }

    public GenericRepository<FileManager> FileManagerRepository
    {
      get
      {
        return new GenericRepository<FileManager>(Context);
      }
    }

    public GenericRepository<FileManagerRole> FileManagerRoleRepository
    {
      get
      {
        return new GenericRepository<FileManagerRole>(Context);
      }
    }

    public GenericRepository<EsSrWorkshopRegion> EsSrWorkshopRegionRepository
    {
      get
      {
        return new GenericRepository<EsSrWorkshopRegion>(Context);
      }
    }


    public GenericRepository<EsSrItemTechnical> EsSrItemTechnicalRepository
    {
      get
      {
        return new GenericRepository<EsSrItemTechnical>(Context);
      }
    }

    public GenericRepository<EsSrAttache> EsSrAttacheRepository
    {
      get
      {
        return new GenericRepository<EsSrAttache>(Context);
      }
    }

    public GenericRepository<EsSrPeriodLock> EsSrPeriodLockRepository
    {
      get
      {
        return new GenericRepository<EsSrPeriodLock>(Context);
      }
    }

    public GenericRepository<EsSrPeriod> EsSrPeriodRepository
    {
      get
      {
        return new GenericRepository<EsSrPeriod>(Context);
      }
    }

    public GenericRepository<EsSrPeriodTechnical> EsSrPeriodTechnicalRepository
    {
      get
      {
        return new GenericRepository<EsSrPeriodTechnical>(Context);
      }
    }

    public GenericRepository<EsSrOrder> EsSrOrderRepository
    {
      get
      {
        return new GenericRepository<EsSrOrder>(Context);
      }
    }

    public GenericRepository<EsSrOrderStage> EsSrOrderStageRepository
    {
      get
      {
        return new GenericRepository<EsSrOrderStage>(Context);
      }
    }

    public GenericRepository<EsSrOrderStageBase> EsSrOrderStageBaseRepository
    {
      get
      {
        return new GenericRepository<EsSrOrderStageBase>(Context);
      }
    }

    public GenericRepository<EsSrOrderStageBaseCatgeory> EsSrOrderStageBaseCatgeoryRepository
    {
      get
      {
        return new GenericRepository<EsSrOrderStageBaseCatgeory>(Context);
      }
    }

    public GenericRepository<EsSrTechnicalWorkDays> EsSrTechnicalWorkDayRepository
    {
      get
      {
        return new GenericRepository<EsSrTechnicalWorkDays>(Context);
      }
    }

    public GenericRepository<EsSrWorkDay> EsSrWorkDayRepository
    {
      get
      {
        return new GenericRepository<EsSrWorkDay>(Context);
      }
    }

    public GenericRepository<EsSrSupervisor> EsSrSupervisorRepository
    {
      get
      {
        return new GenericRepository<EsSrSupervisor>(Context);
      }
    }
    public GenericRepository<EsSrEngineer> EsSrEngineerRepository
    {
      get
      {
        return new GenericRepository<EsSrEngineer>(Context);
      }
    }
    public GenericRepository<xmlController> xmlControllerRepository
    {
      get
      {
        return new GenericRepository<xmlController>(Context);
      }
    }
    public GenericRepository<XmlFile> XmlFileRepository
    {
      get
      {
        return new GenericRepository<XmlFile>(Context);
      }
    }

    public GenericRepository<dataController> dataControllersRepository
    {
      get
      {
        return new GenericRepository<dataController>(Context);
      }
    }

    public GenericRepository<dataController_commands> dataController_commandsRepository
    {
      get
      {
        return new GenericRepository<dataController_commands>(Context);
      }
    }

    public GenericRepository<dataController_commandstableslist> dataController_commandstableslistRepository
    {
      get
      {
        return new GenericRepository<dataController_commandstableslist>(Context);
      }
    }

    public GenericRepository<dataController_fields_field> dataController_fields_fieldRepository
    {
      get
      {
        return new GenericRepository<dataController_fields_field>(Context);
      }
    }

    public GenericRepository<dataController_views> dataController_viewsRepository
    {
      get
      {
        return new GenericRepository<dataController_views>(Context);
      }
    }

  
  }
}
