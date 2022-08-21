using BS.Transactions.Core.Clients;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Clients
{
    public class AccountsServiceClient : IAccountsServiceClient
    {
        private readonly string address;
        
        public AccountsServiceClient(ConfigurationManager configuration)
        {
            address = configuration["AccountsGRPCAddress"];
        }
        public async Task NotifyAccountBalanceUpdated(decimal delta, int accountId, int userId, CancellationToken token = default)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Accounts.Service.Account.AccountClient(channel);
                //Should be fire and forget
                await client.AccountBalanceChangedAsync(new Accounts.Service.AccountBalanceChangedRequest()
                {
                    AccountId = accountId,
                    Delta = (double)delta,
                    UserId = userId
                }, cancellationToken: token);
            }
        }
    }
}
