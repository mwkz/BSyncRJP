using BS.Common.Core.Services;
using BS.Security.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Core.Services
{
    public interface IAuthenticateUserService : IBusinessService<AuthenticateUserRequest, AuthenticateUserResponse>
    {

    }
}
