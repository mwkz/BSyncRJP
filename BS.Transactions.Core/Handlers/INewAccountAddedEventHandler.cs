using BS.Common.Core.Events;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Handlers
{
    public interface INewAccountAddedEventHandler : IEventHandler<NewAccountAddedEvent>
    {

    }
}
