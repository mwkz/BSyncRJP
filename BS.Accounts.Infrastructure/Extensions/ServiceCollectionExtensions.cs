using BS.Accounts.Core.Clients;
using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Handlers;
using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using BS.Accounts.Core.Services;
using BS.Accounts.Infrastructure.Clients;
using BS.Accounts.Infrastructure.Handlers;
using BS.Accounts.Infrastructure.Repositories;
using BS.Accounts.Infrastructure.Repositories.DBContexts;
using BS.Accounts.Infrastructure.Services;
using BS.Accounts.Infrastructure.Validators;
using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
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
        public static IServiceCollection AddAccounts(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddScoped<DbContext, AccountsDBContext>();
            services.AddDbContext<AccountsDBContext>(options =>
            {
                options.UseSqlite(configurationManager.GetConnectionString("MainDBConnection"));
            });


            services.AddScoped<IAccountsUnitOfWork, AccountsUnitOfWork>();
            services.AddScoped<IAddAccountService, AddAccountService>();
            services.AddScoped<IGetAccountService, GetAccountService>();
            services.AddScoped<IGetAccountsService, GetAccountsService>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IAccountBalanceChangedEventHandler, AccountBalanceChangedEventHandler>();
            services.AddScoped<IAccountsTransactionsServiceClient, AccountsTransactionsServiceClient>();


            services.AddScoped<IValidator<AddAccountRequest>, AddAccountRequestValidator>();
            services.AddScoped(_ => configurationManager);

            return services;            
        }
    }
}
