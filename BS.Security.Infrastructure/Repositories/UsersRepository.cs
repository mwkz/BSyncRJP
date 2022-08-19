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
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DbContext dbContext) 
            : base(dbContext)
        { }
    }
}
