using BS.Common.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BS.Common.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        internal DbContext DbContext { get; }

        public async Task<ITransaction> BeginTransaction(CancellationToken token = default)
        {
            IDbContextTransaction dbTransaction = await DbContext.Database.BeginTransactionAsync(token);
            return new Transaction(dbTransaction);
        }

        public async Task SaveChanges(CancellationToken token = default)
        {
            await DbContext.SaveChangesAsync(token);
        }
    }
}
