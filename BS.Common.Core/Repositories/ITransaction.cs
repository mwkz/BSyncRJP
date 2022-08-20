using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Repositories
{
    public interface ITransaction : IDisposable 
    {
        Task Complete(CancellationToken token = default);
    }
}
