using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Clients
{
    public interface IAccountsServiceClient
    {
        Task NotifyAccountBalanceUpdated(decimal delta, int accountId, int userId, CancellationToken token = default);
    }
}
