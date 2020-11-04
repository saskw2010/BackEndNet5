using BackEnd.BAL.Repository;
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

    }
}
