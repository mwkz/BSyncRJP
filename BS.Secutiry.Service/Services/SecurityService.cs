using BS.Security.Service;
using Grpc.Core;

namespace BS.Secutiry.Service.Services
{
    public class SecurityService : Security.Service.Security.SecurityBase
    {
        public SecurityService()
        {

        }

        public override Task<GetUserReply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            return base.GetUser(request, context);
        }
    }
}
