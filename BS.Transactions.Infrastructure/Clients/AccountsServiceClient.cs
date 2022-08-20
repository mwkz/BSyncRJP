using BS.Transactions.Core.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Clients
{
    public class AccountsServiceClient : IAccountsServiceClient
    {
        public Task NotifyAccountBalanceUpdated(decimal delta, int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
