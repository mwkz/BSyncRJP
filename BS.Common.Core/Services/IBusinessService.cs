using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Services
{
    public interface IBusinessService<Request, Response>        
    {
        Task<BusinessServiceResponse<Response>> Execute(BusinessServiceRequest<Request> request, CancellationToken token = default);
    }
}
