using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Context;

namespace BackEnd.BAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal BakEndContext Context;

        internal DbSet<T> dbSet;

        public GenericRepository(BakEndContext Context)
        {
            this.Context = Context;
            this.dbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0,int Take=10 , string NoTrack = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (NoTrack != "")
            {
                query = query.AsNoTracking();
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                // Paging
                if (page > 0)
                {
                    return query.ToList().Skip((page - 1)*Take).Take(Take);
                }
                else
                    return query.ToList();
            }
        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetEntity(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.SingleOrDefault();

        }

        public virtual async Task<T> GetAsyncByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(T Entity)
        {
            dbSet.Add(Entity);
        }

        public virtual T Last()
        {
            return dbSet.LastOrDefault();
        }
        public int Count()
        {
            return dbSet.Count();
        }

        public virtual void Update(T EntityToUpdate)
        {
            dbSet.Attach(EntityToUpdate);
            Context.Entry(EntityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T EntityToDelete = dbSet.Find(id);
            Delete(EntityToDelete);
        }

        public virtual void Delete(T EntityToDelete)
        {
            if (Context.Entry(EntityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(EntityToDelete);
            }
            dbSet.Remove(EntityToDelete);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public void RemovRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void RemovRange(IEnumerable<T> entities, string NoTrack = "")
        {
            Context.Set<T>().RemoveRange(entities);
        }



        public virtual IEnumerable<T> GetMobilApp(Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "", int page = 0, string NoTrack = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (NoTrack != "")
            {
                query = query.AsNoTracking();
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                // Paging
                if (page > 0)
                {
                    page = page * 5;
                    return query.ToList().Skip(page - 5).Take(5);
                }
                else
                    return query.ToList();
            }
        }

    }
}
