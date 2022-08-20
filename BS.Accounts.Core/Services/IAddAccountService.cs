using BS.Accounts.Core.DTO;
using BS.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Services
{
    public interface IAddAccountService : IBusinessService<AddAccountRequest, AddAccountResponse>
    {
    }
}
