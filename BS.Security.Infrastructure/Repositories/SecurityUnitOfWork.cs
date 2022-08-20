using BS.Common.Core.Repositories;
using BS.Common.Infrastructure.Repositories;
using BS.Security.Core.Models;
using BS.Security.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Infrastructure.Repositories
{
    public class SecurityUnitOfWork : UnitOfWork, ISecurityUnitOfWork
    {
        private readonly IRepository<User> users;

        public SecurityUnitOfWork(DbContext dbContext, IRepository<User> users) 
            : base(dbContext)
        {
            this.users = users;
        }

        public IRepository<User> Users => users;
    }
}
