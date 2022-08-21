using BS.Accounts.Core.Clients;
using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Repositories;
using BS.Accounts.Core.Services;
using BS.Accounts.Infrastructure.Repositories;
using BS.Accounts.Infrastructure.Repositories.DBContexts;
using BS.Accounts.Infrastructure.Services;
using BS.Accounts.Infrastructure.Validators;
using BS.Common.Core.Services;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BS.Accounts.Tests
{
    [TestClass]
    public class AddAccountTest
    {

        private ServiceProvider provider;
        private DbContext dbContext;
        private decimal initialBalance;

        [TestInitialize]
        public void Initialize()
        {
            ServiceCollection services = new ServiceCollection();

            var connection = new SqliteConnection("Filename=:memory:");
            var contextOptions = new DbContextOptionsBuilder<AccountsDBContext>()
                .UseSqlite(connection).Options;
            
            dbContext = new AccountsDBContext(contextOptions);
            services.AddSingleton(dbContext);
            services.AddSingleton<DbContext>(c => dbContext);

            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();

            services.AddSingleton<IAccountsUnitOfWork, AccountsUnitOfWork>();
            services.AddSingleton<IValidator<AddAccountRequest>, AddAccountRequestValidator>();
            services.AddSingleton<IAccountsRepository, AccountsRepository>();
            services.AddSingleton<IAddAccountService, AddAccountService>();
            services.AddAutoMapper(typeof(AccountsRepository).Assembly);
            services.AddSingleton<IAccountsTransactionsServiceClient>(factory =>
            {
                
                var mock = new Moq.Mock<IAccountsTransactionsServiceClient>();
                mock.Setup(x => x.NotifyAccountAdded(It.IsAny<int>(), It.IsAny<decimal>(), 1, default))
                        .Callback<int, decimal, int, CancellationToken>( (id, balance, userId, token) => { initialBalance = balance; });
                return mock.Object;
            });
            
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddLogging();




            provider = services.BuildServiceProvider();
       }

        [TestCleanup]
        public void Cleanup()
        {            
            dbContext?.Dispose();
            provider?.Dispose();
        }

        [TestMethod]
        public async Task Add_ValidAccount_Success()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.IsNotNull(result.Response);
            Assert.IsTrue(result.Response?.Name?.Equals("Test Account"));
            Assert.IsTrue(result.Response?.AccountNo?.Equals("1000"));
            Assert.AreEqual(result?.Response?.CustomerId, 1);
            Assert.AreEqual(result?.Response?.Balance, 0M);
        }

        [TestMethod]
        public async Task Add_AccountInvalidName_Fail()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = ""
                    }, 1)
            );

            //assert
            Assert.IsFalse(result.Succeeded);

        }

        [TestMethod]
        public async Task Add_AccountInvalidAccountNo_Fail()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );

            //assert
            Assert.IsFalse(result.Succeeded);

        }

        [TestMethod]
        public async Task Add_AccountInvalidCustomerId_Fail()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 0,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );

            //assert
            Assert.IsFalse(result.Succeeded);

        }

        [TestMethod]
        public async Task Add_AccountExistingAccountNo_Fail()
        {
            //prepare
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );

            var result2 = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );


            //assert
            Assert.IsFalse(result2.Succeeded);
        }

        [TestMethod]
        public async Task Add_AccountWithInitalBalance_Success()
        {
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 100M,
                        Name = "Test Account"
                    }, 1)
            );

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(initialBalance, 100M);

        }

        [TestMethod]
        public async Task Add_AccountWithoutInitalBalance_Success()
        {
            var service = provider.GetRequiredService<IAddAccountService>();

            //execute
            var result = await service.Execute(
                    new BusinessServiceRequest<AddAccountRequest>(new AddAccountRequest()
                    {
                        AccountNo = "1000",
                        CustomerId = 1,
                        InitialBalance = 0M,
                        Name = "Test Account"
                    }, 1)
            );

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(result?.Response?.Balance, 0M);
        }

    }
}