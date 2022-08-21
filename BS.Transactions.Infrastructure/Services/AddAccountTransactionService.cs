using AutoMapper;
using BS.Common.Core.Services;
using BS.Common.Infrastructure.Services;
using BS.Transactions.Core.Clients;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Models;
using BS.Transactions.Core.Repositories;
using BS.Transactions.Core.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Services
{
    public class AddAccountTransactionService : BusinessService<AddAccountTransactionRequest, AddAccountTransactionResponse>, IAddAccountTransactionService
    {
        private readonly IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork;
        private readonly IAccountsServiceClient accountsServiceClient;
        private readonly IMapper mapper;

        public AddAccountTransactionService(IEnumerable<IValidator<AddAccountTransactionRequest>> validators, 
                                            IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork,
                                            IAccountsServiceClient accountsServiceClient,
                                            IMapper mapper,
                                            ILogger<AddAccountTransactionService> logger) 
            : base(validators, logger)
        {
            this.accountsTransactionsUnitOfWork = accountsTransactionsUnitOfWork;
            this.accountsServiceClient = accountsServiceClient;
            this.mapper = mapper;
        }

        protected override async Task<AddAccountTransactionResponse> ExecuteRequest(BusinessServiceRequest<AddAccountTransactionRequest> request, CancellationToken token = default)
        {
            var transaction = mapper.Map<AddAccountTransactionRequest, AccountTransaction>(request.Request);

            transaction.UpdatedDate = DateTime.UtcNow;
            transaction.CreatedDate = DateTime.UtcNow;
            transaction.CreatedByUserId = request.UserId;
            transaction.UpdatedByUserId = request.UserId;

            if (request.Request.Value > 0)
            {
                transaction.Credit = Math.Abs(request.Request.Value);
                transaction.Debit = 0;
            }
            else
            {
                transaction.Debit = Math.Abs(request.Request.Value);
                transaction.Credit = 0;
            }

            accountsTransactionsUnitOfWork.AccountsTransactions.Add(transaction);

            var balance = accountsTransactionsUnitOfWork.AccountsBalances.Get().First(b => b.AccountId == request.Request.AccountId);
            balance.Balance += request.Request.Value;

            if (request.Request.IsInitial == false)
                await accountsServiceClient.NotifyAccountBalanceUpdated(request.Request.Value, request.Request.AccountId, request.UserId, token).ConfigureAwait(false);

            await accountsTransactionsUnitOfWork.SaveChanges(token).ConfigureAwait(false);

            return mapper.Map<AccountTransaction, AddAccountTransactionResponse>(transaction);
        }
    }
}
