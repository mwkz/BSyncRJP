using BS.Common.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;            
        }

        public DbContext DbContext { get; }

        public T Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            var set = DbContext.Set<T>();

            if (DbContext.Entry(entity).State == EntityState.Detached)
                set.Attach(entity);

            return entity;
        }

        public IQueryable<T> Get()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public T Update(T entity)
        {
            var set = DbContext.Set<T>();
            if (DbContext.Entry(entity).State == EntityState.Detached)
                set.Update(entity);
            return entity;
        }
    }
}
