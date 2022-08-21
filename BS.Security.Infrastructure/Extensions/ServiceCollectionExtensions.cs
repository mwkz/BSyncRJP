using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using BS.Security.Core.Models;
using BS.Security.Core.Repositories;
using BS.Security.Core.Services;
using BS.Security.Infrastructure.Repositories;
using BS.Security.Infrastructure.Repositories.DBContexts;
using BS.Security.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddScoped<DbContext, SecurityDBContext>();
            services.AddDbContext<SecurityDBContext>(options =>
            {
                options.UseSqlite(configurationManager.GetConnectionString("MainDBConnection"));
            });


            services.AddScoped<ISecurityUnitOfWork, SecurityUnitOfWork>();
            services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
            services.AddScoped<IRepository<User>, Repository<User>>();            

            return services;            
        }
    }
}
