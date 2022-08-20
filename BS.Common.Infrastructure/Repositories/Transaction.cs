using BS.Common.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Infrastructure.Repositories
{
    public class Transaction : ITransaction, IDisposable
    {
        private readonly IDbContextTransaction transaction;
            
        public Transaction(IDbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        private bool disposedValue;


        public async Task Complete(CancellationToken token = default)
        {
            await this.transaction.CommitAsync(token);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    transaction.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
