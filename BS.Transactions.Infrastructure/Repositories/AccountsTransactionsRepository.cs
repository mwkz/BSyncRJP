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
    public class AccountsTransactionsRepository : Repository<AccountTransaction>, IAccountsTransactionsRepository
    {
        public AccountsTransactionsRepository(DbContext dbContext) 
            : base(dbContext)
        { }

        public IQueryable<AccountTransaction> GetAccountTransactions(int accountId)
        {
            return DbContext.Set<AccountTransaction>()
                            .AsNoTracking()
                            .Where(item => item.AccountId == accountId);
        }
    }
}
