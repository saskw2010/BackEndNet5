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
                try
                {
                    await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
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
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = 500;
                    dbContextTransaction.Rollback();
                }
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

    //end workspace





  }
}
