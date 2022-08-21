using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using BS.Transactions.Core.Clients;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Handlers;
using BS.Transactions.Core.Models;
using BS.Transactions.Core.Repositories;
using BS.Transactions.Core.Services;
using BS.Transactions.Infrastructure.Clients;
using BS.Transactions.Infrastructure.Handlers;
using BS.Transactions.Infrastructure.Repositories;
using BS.Transactions.Infrastructure.Repositories.DBContexts;
using BS.Transactions.Infrastructure.Services;
using BS.Transactions.Infrastructure.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountsTransactions(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddScoped<DbContext, AccountsTransactionsDBContext>();
            services.AddDbContext<AccountsTransactionsDBContext>(options =>
            {
                options.UseSqlite(configurationManager.GetConnectionString("MainDBConnection"));
            });


            services.AddScoped<IAccountsTransactionsUnitOfWork, AccountsTransactionsUnitOfWork>();
            services.AddScoped<IAddAccountTransactionService, AddAccountTransactionService>();
            services.AddScoped<IGetAccountTransactionsService, GetAccountTransactionsService>();
            services.AddScoped<IAccountsTransactionsRepository, AccountsTransactionsRepository>();
            services.AddScoped<IRepository<AccountBalance>, Repository<AccountBalance>>();
            
            services.AddScoped<INewAccountAddedEventHandler, NewAccountAddedEventHandler>();
            services.AddScoped<IAccountsServiceClient, AccountsServiceClient>();


            services.AddScoped<IValidator<AddAccountTransactionRequest>, AddAccountTransactionRequestValidator>();
            services.AddScoped(_ => configurationManager);

            return services;            
        }
    }
}
