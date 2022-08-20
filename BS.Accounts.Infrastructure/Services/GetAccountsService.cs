using AutoMapper;
using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using BS.Accounts.Core.Services;
using BS.Common.Core.Services;
using BS.Common.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Services
{
    public class GetAccountsService : BusinessService<GetAccountsRequest, IEnumerable<GetAccountsResponse>>, IGetAccountsService
    {
        private readonly IAccountsUnitOfWork accountsUnitOfWork;
        private readonly IMapper mapper;

        public GetAccountsService(IEnumerable<IValidator<GetAccountsRequest>> validators, 
                                  IAccountsUnitOfWork accountsUnitOfWork,
                                  IMapper  mapper,
                                  ILogger<GetAccountsService> logger) 
            : base(validators, logger)
        {
            this.accountsUnitOfWork = accountsUnitOfWork;
            this.mapper = mapper;
        }

        protected override async Task<IEnumerable<GetAccountsResponse>> ExecuteRequest(BusinessServiceRequest<GetAccountsRequest> request, CancellationToken token = default)
        {
            var accounts = await accountsUnitOfWork.Accounts.Get()
                                             .AsNoTracking().ToListAsync(token).ConfigureAwait(false);

            return mapper.Map<IEnumerable<Account>, IEnumerable<GetAccountsResponse>>(accounts);
            
        }
    }
}
