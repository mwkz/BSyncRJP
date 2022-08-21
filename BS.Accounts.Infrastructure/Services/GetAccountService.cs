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
    public class GetAccountService : BusinessService<GetAccountRequest, GetAccountResponse?>, IGetAccountService
    {
        private readonly IAccountsUnitOfWork accountsUnitOfWork;
        private readonly IMapper mapper;

        public GetAccountService(IEnumerable<IValidator<GetAccountRequest>> validators,
                                              IAccountsUnitOfWork accountsUnitOfWork,
                                              IMapper mapper,
                                              ILogger<GetAccountService> logger) 
            : base(validators, logger)
        {
            this.accountsUnitOfWork = accountsUnitOfWork;
            this.mapper = mapper;
        }

        protected override async Task<GetAccountResponse?> ExecuteRequest(BusinessServiceRequest<GetAccountRequest> request, CancellationToken token = default)
        {
            var account = await accountsUnitOfWork.Accounts.Get()
                                                  .AsNoTracking()
                                                  .Where(a => a.Id == request.Request.AccountId)
                                                  .FirstOrDefaultAsync(token).ConfigureAwait(false);

            return mapper.Map<Account?, GetAccountResponse?>(account);
        }
    }
}
