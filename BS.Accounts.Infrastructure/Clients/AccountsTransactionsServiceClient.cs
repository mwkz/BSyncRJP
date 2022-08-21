using BS.Accounts.Core.Clients;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Clients
{
    public class AccountsTransactionsServiceClient : IAccountsTransactionsServiceClient
    {
        private readonly string address;

        public AccountsTransactionsServiceClient(ConfigurationManager configuration)
        {
            address = configuration["AccountsTransactionsGRPCAddress"];
        }

        public async Task NotifyAccountAdded(int accountId, decimal initialBalance, int userId, CancellationToken token = default)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Transactions.Service.AccountsTransactions.AccountsTransactionsClient(channel);
                await client.NewAccountAddedAsync(new Transactions.Service.NewAccountAddedRequest()
                {
                    AccountId = accountId,
                    InitialBalance = (double)initialBalance,
                    UserID = userId
                }, cancellationToken: token);
            }

            
        }
    }
}
