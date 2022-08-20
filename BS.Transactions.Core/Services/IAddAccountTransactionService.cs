using BS.Common.Core.Services;
using BS.Transactions.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Services
{
    public interface IAddAccountTransactionService : IBusinessService<AddAccountTransactionRequest, AddAccountTransactionResponse>
    {
    }
}
