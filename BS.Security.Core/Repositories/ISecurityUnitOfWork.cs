using BS.Common.Core.Repositories;
using BS.Security.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Core.Repositories
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IRepository<User> Users { get; }
    }
}
