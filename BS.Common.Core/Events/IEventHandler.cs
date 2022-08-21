using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Events
{
    public interface IEventHandler<Event>
    {
        Task HandleEvent(Event e, CancellationToken token = default);
    }
}
