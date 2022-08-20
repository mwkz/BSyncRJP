using BS.Common.Core.Services;
using BS.Security.Core.Services;
using BS.Security.Service;
using Grpc.Core;
using Grpc.Net.Client;
using DTO = BS.Security.Core.DTO;

namespace BS.Secutiry.Service.Services
{
    public class SecurityService : Security.Service.Security.SecurityBase
    {
        private readonly IAuthenticateUserService authenticUserService;

        public SecurityService(IAuthenticateUserService authenticUserService)
        {
            this.authenticUserService = authenticUserService;
        }

        public override async Task<AuthenticateUserReply> AuthenticaUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            BusinessServiceRequest<DTO.AuthenticateUserRequest> businessRequest = new BusinessServiceRequest<DTO.AuthenticateUserRequest>(new DTO.AuthenticateUserRequest()
            {
                Username = request.Username,
                Password = request.Password
            }, 0);

            var result = await authenticUserService.Execute(businessRequest, context.CancellationToken);
            
            if (result != null && result.Response != null)
                return new AuthenticateUserReply() { Authenticated = result.Response.Authenticated, Id = result.Response.Id, Username = result.Response.Username };
            else
                return new AuthenticateUserReply() { Authenticated = false, Id = null, Username = null };

        }

    }
}
