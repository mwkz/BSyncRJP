using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Events;
using BS.Common.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Handlers
{
    public interface IAccountBalanceChangedEventHandler : IEventHandler<AccountBalanceChangedEvent>
    {

    }
}
