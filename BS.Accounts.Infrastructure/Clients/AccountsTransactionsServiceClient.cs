using BS.Accounts.Core.Clients;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
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

        public AccountsTransactionsServiceClient(WebApplicationBuilder webApplicationBuilder)
        {
            address = webApplicationBuilder.Configuration["AccountsTransactionsGRPCAddress"];
        }

        public async Task NotifyAccountAdded(int accountId, decimal initialBalance, CancellationToken token = default)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Transactions.Service.AccountsTransactions.AccountsTransactionsClient(channel);
                await client.NewAccountAddedAsync(new Transactions.Service.NewAccountAddedRequest()
                {
                    AccountId = accountId,
                    InitialBalance = (double)initialBalance
                }, cancellationToken: token);
            }

            
        }
    }
}
