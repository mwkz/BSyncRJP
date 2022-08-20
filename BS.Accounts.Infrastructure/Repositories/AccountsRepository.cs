using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Repositories
{
    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {
        public AccountsRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<bool> AccountExists(string? accountNo, CancellationToken token = default)
        {
            return await DbContext.Set<Account>().AnyAsync(a => a.AccountNo == accountNo, token).ConfigureAwait(false);
        }
    }
}
