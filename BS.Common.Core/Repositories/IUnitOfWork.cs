using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChanges(CancellationToken token = default);
        Task<ITransaction> BeginTransaction(CancellationToken token = default);
        
    }
}
