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
    public class GetUserService : BusinessService<GetUserRequest, GetUserResponse?>, IGetUserService
    {
        private readonly ISecurityUnitOfWork securityUnitOfWork;
        private readonly IMapper mapper;

        public GetUserService(IEnumerable<IValidator<GetUserRequest>> validators, 
                              ISecurityUnitOfWork securityUnitOfWork,
                              IMapper mapper,
                              ILogger<GetUserService> logger) 
            : base(validators, logger)
        {
            this.securityUnitOfWork = securityUnitOfWork;
            this.mapper = mapper;
        }

        protected override async Task<GetUserResponse?> ExecuteRequest(BusinessServiceRequest<GetUserRequest> request, CancellationToken token = default)
        {
            var user =await securityUnitOfWork.Users.Get()
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(u => u.Id == request.Request.UserId, token);

            return mapper.Map<User?, GetUserResponse?>(user);
        }
    }
}
