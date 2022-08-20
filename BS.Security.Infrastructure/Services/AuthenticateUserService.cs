using AutoMapper;
using BS.Common.Core.Services;
using BS.Common.Infrastructure.Services;
using BS.Security.Core.DTO;
using BS.Security.Core.Models;
using BS.Security.Core.Repositories;
using BS.Security.Core.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Infrastructure.Services
{
    public class AuthenticateUserService : BusinessService<AuthenticateUserRequest, AuthenticateUserResponse>, IAuthenticateUserService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly IMapper mapper;

        public AuthenticateUserService(IEnumerable<IValidator<AuthenticateUserRequest>> validators, 
                              ISecurityUnitOfWork securityUnitOfWork,
                              IMapper mapper,
                              ILogger<AuthenticateUserService> logger) 
            : base(validators, logger)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.mapper = mapper;
        }

        protected override async Task<AuthenticateUserResponse> ExecuteRequest(BusinessServiceRequest<AuthenticateUserRequest> request, CancellationToken token = default)
        {
            var user = await securityUnitOfWork.Users.Get()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Username == request.Request.Username && 
                                                          u.Password == request.Request.Password && 
                                                          u.Enabled);

            if (user == null)
                return new AuthenticateUserResponse() { Authenticated = false };
            else
                return new AuthenticateUserResponse() { Authenticated = true, Id = user.Id, Username = user.Username };
        }
    }
}
