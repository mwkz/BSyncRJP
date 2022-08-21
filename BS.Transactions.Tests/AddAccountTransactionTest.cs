using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using BS.Transactions.Infrastructure.Repositories.DBContexts;
using BS.Transactions.Infrastructure.Repositories;
using BS.Transactions.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BS.Transactions.Core.DTO;
using BS.Transactions.Infrastructure.Validators;
using BS.Transactions.Core.Services;
using BS.Transactions.Infrastructure.Services;
using BS.Transactions.Core.Clients;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BS.Common.Core.Services;
using Microsoft.Data.Sqlite;
using BS.Transactions.Core.Models;
using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using System.Threading;

namespace BS.Transactions.Tests
{
    

    [TestClass]
    public class AddAccountTransactionTest
    {
        private ServiceProvider provider;
        private DbContext dbContext;

        private decimal delta;

        [TestInitialize]
        public void Initialize()
        {
            ServiceCollection services = new ServiceCollection();

            var connection = new SqliteConnection("Filename=:memory:");

            var contextOptions = new DbContextOptionsBuilder<AccountsTransactionsDBContext>()
                .UseSqlite(connection).Options;

            dbContext = new AccountsTransactionsDBContext(contextOptions);
            services.AddSingleton<AccountsTransactionsDBContext>((AccountsTransactionsDBContext)dbContext);            
            services.AddSingleton<DbContext>(c => dbContext);

            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();



            services.AddSingleton<IAccountsTransactionsUnitOfWork, AccountsTransactionsUnitOfWork>();
            services.AddSingleton<IValidator<AddAccountTransactionRequest>, AddAccountTransactionRequestValidator>();
            services.AddSingleton<IRepository<AccountBalance>, Repository<AccountBalance>>();
            services.AddSingleton<IAccountsTransactionsRepository, AccountsTransactionsRepository>();
            services.AddSingleton<IAddAccountTransactionService, AddAccountTransactionService>();
            services.AddAutoMapper(typeof(AccountsTransactionsRepository).Assembly);
            services.AddSingleton<IAccountsServiceClient>(factory =>
            {

                var mock = new Moq.Mock<IAccountsServiceClient>();
           
                mock.Setup(x => x.NotifyAccountBalanceUpdated(It.IsAny<decimal>(), It.IsAny<int>(), 1, default))
                        .Callback<decimal, int, int, CancellationToken>((id, balance, userId, token) => { delta = balance; });
                return mock.Object;
            });

            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddLogging();


            provider = services.BuildServiceProvider();
            provider.GetRequiredService<AccountsTransactionsDBContext>().Database.EnsureCreated();

        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext?.Dispose();
            provider?.Dispose();
        }

        [TestMethod]
        public async Task Add_ValidAccountTransaction_Success()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountTransactionService>();
            var uow = provider.GetRequiredService<IAccountsTransactionsUnitOfWork>();
            uow.AccountsBalances.Add(new Core.Models.AccountBalance() { AccountId = 1, Balance = 0 });
            await uow.SaveChanges();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountTransactionRequest>(new AddAccountTransactionRequest()
                    {
                        AccountId = 1,
                        IsInitial = true,
                        Value = 1000
                    }, 1)
            );

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(result.Response?.Credit, 1000M);
            Assert.AreEqual(result.Response?.Debit, 0M);
        }

        [TestMethod]
        public async Task Add_AccountBalanceUpdate_Success()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountTransactionService>();
            var uow = provider.GetRequiredService<IAccountsTransactionsUnitOfWork>();
            uow.AccountsBalances.Add(new Core.Models.AccountBalance() { AccountId = 1, Balance = 0 });
            await uow.SaveChanges();

            //execute
            await service.Execute(
                    new BusinessServiceRequest<AddAccountTransactionRequest>(new AddAccountTransactionRequest()
                    {
                        AccountId = 1,
                        IsInitial = true,
                        Value = 1000
                    }, 1));

            await service.Execute(
                    new BusinessServiceRequest<AddAccountTransactionRequest>(new AddAccountTransactionRequest()
                    {
                        AccountId = 1,
                        IsInitial = false,
                        Value = 1000
                    }, 1));

            var accountBalance = await uow.AccountsBalances.Get().AsNoTracking().FirstAsync(b => b.AccountId == 1);

            //assert
            Assert.IsTrue(accountBalance.Balance == 2000M);
 
        }

        [TestMethod]
        public async Task Add_AccountTransactionInvalidBalance_Fail()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountTransactionService>();
            var uow = provider.GetRequiredService<IAccountsTransactionsUnitOfWork>();
            uow.AccountsBalances.Add(new Core.Models.AccountBalance() { AccountId = 1, Balance = 0 });
            await uow.SaveChanges();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountTransactionRequest>(new AddAccountTransactionRequest()
                    {
                        AccountId = 1,
                        IsInitial = true,
                        Value = 0
                    }, 1)
            );

            //assert
            Assert.IsFalse(result.Succeeded);

        }


    }
}