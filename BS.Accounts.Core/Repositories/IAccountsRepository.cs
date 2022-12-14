using BS.Accounts.Core.Models;
using BS.Common.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Repositories
{
    public interface IAccountsRepository : IRepository<Account>
    {
        Task<bool> AccountExists(string? accountNo, CancellationToken token = default);
    }
}
