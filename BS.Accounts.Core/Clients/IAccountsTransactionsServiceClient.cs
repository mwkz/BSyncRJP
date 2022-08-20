using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Clients
{
    public interface IAccountsTransactionsServiceClient
    {
        Task NotifyAccountAdded(int accountId, decimal initialBalance, CancellationToken token = default);
    }
}
