using BS.Common.Core.Repositories;
using BS.Transactions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Repositories
{
    public interface IAccountsTransactionsUnitOfWork : IUnitOfWork
    {
        IAccountsTransactionsRepository AccountsTransactions { get; }
    }
}
