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

        public async Task<T> Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(entity).Reload();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            var set = DbContext.Set<T>();

            if (DbContext.Entry(entity).State == EntityState.Detached)
                set.Attach(entity);

            set.Remove(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public IEnumerable<T> Get()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task<T> Update(T entity)
        {
            var set = DbContext.Set<T>();

            if (DbContext.Entry(entity).State == EntityState.Detached)
                set.Update(entity);

            await DbContext.SaveChangesAsync();
            DbContext.Entry(entity).Reload();

            return entity;

        }
    }
}
