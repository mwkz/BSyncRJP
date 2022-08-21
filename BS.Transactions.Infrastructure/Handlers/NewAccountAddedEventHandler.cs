using BS.Common.Core.Services;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Events;
using BS.Transactions.Core.Handlers;
using BS.Transactions.Core.Models;
using BS.Transactions.Core.Repositories;
using BS.Transactions.Core.Services;
using BS.Transactions.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Handlers
{
    public class NewAccountAddedEventHandler : INewAccountAddedEventHandler
    {
        private readonly IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork;
        private readonly IAddAccountTransactionService addAccountTransactionService;

        public NewAccountAddedEventHandler(IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork, IAddAccountTransactionService addAccountTransactionService)
        {
            this.accountsTransactionsUnitOfWork = accountsTransactionsUnitOfWork;
            this.addAccountTransactionService = addAccountTransactionService;
        }


        public async Task HandleEvent(NewAccountAddedEvent e, CancellationToken token = default)
        {
            using (var transaction = await accountsTransactionsUnitOfWork.BeginTransaction(token))
            {

                accountsTransactionsUnitOfWork.AccountsBalances.Add(
                        new AccountBalance()
                        {
                            AccountId = e.AccountId,
                            Balance = e.InitialBalance
                        }
                    );

                await accountsTransactionsUnitOfWork.SaveChanges(token);

                if (e.InitialBalance != 0)
                {
                    await addAccountTransactionService.Execute(new BusinessServiceRequest<AddAccountTransactionRequest>
                        (
                            new AddAccountTransactionRequest() { AccountId = e.AccountId, IsInitial = true, Value = e.InitialBalance },
                            e.UserId
                        ), token);
                }

                await transaction.Complete(token);
            }
        }
    }
}
