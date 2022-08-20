using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using BS.Transactions.Core.Models;
using BS.Transactions.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Repositories
{
    public class AccountsTransactionsUnitOfWork : UnitOfWork, IAccountsTransactionsUnitOfWork
    {
        private readonly IAccountsTransactionsRepository accountsTransactionsRepository;
        private readonly IRepository<AccountBalance> accountsBalancesRepository;

        public AccountsTransactionsUnitOfWork(DbContext dbContext, 
                                            IAccountsTransactionsRepository accountsTransactionsRepository,
                                            IRepository<AccountBalance> accountsBalancesRepository) 
            : base(dbContext)
        {
            this.accountsTransactionsRepository = accountsTransactionsRepository;
            this.accountsBalancesRepository = accountsBalancesRepository;
        }

        public IAccountsTransactionsRepository AccountsTransactions => accountsTransactionsRepository;
        public IRepository<AccountBalance> AccountsBalances => accountsBalancesRepository;

    }
}
