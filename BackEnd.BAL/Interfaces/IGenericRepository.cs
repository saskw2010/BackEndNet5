using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, string NoTrack = "");

        T GetByID(object id);

        T GetEntity(Expression<Func<T, bool>> filter);

        T Last();

        int Count();

        Task<T> GetAsyncByID(object id);

        void Insert(T entity);


        void Update(T EntityToUpdate);


        void Delete(object id);


        void Delete(T EntityToDelete);

        void AddRange(IEnumerable<T> entities);
        void RemovRange(IEnumerable<T> entities, string NoTrack = "");



    }
}
