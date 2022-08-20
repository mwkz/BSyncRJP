using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Repositories
{
    public interface IRepository<T> 
        where T : class
    {
        IQueryable<T> Get();
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
