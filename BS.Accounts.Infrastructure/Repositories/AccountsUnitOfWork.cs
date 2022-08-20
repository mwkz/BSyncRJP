using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Repositories
{
    public class AccountsUnitOfWork : UnitOfWork, IAccountsUnitOfWork
    {
        private readonly IAccountsRepository accounts;

        public AccountsUnitOfWork(DbContext dbContext, IAccountsRepository accounts)
            : base(dbContext)
        {
            this.accounts = accounts;
        }

        public IAccountsRepository Accounts => accounts;
    }
}
