using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Events;
using BS.Accounts.Core.Handlers;
using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Handlers
{
    public class AccountBalanceChangedEventHandler : IAccountBalanceChangedEventHandler
    {
        private readonly IAccountsUnitOfWork accountsUnitOfWork;

        public AccountBalanceChangedEventHandler(IAccountsUnitOfWork accountsUnitOfWork )
        {
            this.accountsUnitOfWork = accountsUnitOfWork;
        }

        public async Task HandleEvent(AccountBalanceChangedEvent e, CancellationToken token = default)
        {
            var account = await accountsUnitOfWork.Accounts.Get()
                                        .FirstOrDefaultAsync(a => a.Id == e.AccountId, token).ConfigureAwait(false);

            if (account == null)
                return;

            account.Balance += e.Delta;

            try
            {
                await accountsUnitOfWork.SaveChanges().ConfigureAwait(false);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                //Detect optimistic concurrency issues, this should code should be enhanced a made more generic
                foreach(var entry in ex.Entries)
                {
                    if (entry.Entity is Account accountEntry)
                    {
                        var updatedValue = entry.CurrentValues[nameof(Account.Balance)];
                        var dbValues = await entry.GetDatabaseValuesAsync(token).ConfigureAwait(false);

                        if (dbValues != null)
                        {
                            var dbValue = dbValues[nameof(Account.Balance)];
                            if (dbValue != null && dbValue != updatedValue)
                            {
                                entry.OriginalValues.SetValues(dbValue);
                                account.Balance += e.Delta;
                                await accountsUnitOfWork.SaveChanges().ConfigureAwait(false);
                                return;
                            }
                        }
                    }
                }

                throw;
            }

        }
    }
}
