using AutoMapper;
using BS.Common.Core.Services;
using BS.Common.Infrastructure.Services;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Models;
using BS.Transactions.Core.Repositories;
using BS.Transactions.Core.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Services
{
    public class GetAccountTransactionsService : BusinessService<GetAccountTransactionsRequest, IEnumerable<GetAccountTransactionsResponse>>, IGetAccountTransactionsService
    {
        private readonly IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork;
        private readonly IMapper mapper;

        public GetAccountTransactionsService(IEnumerable<IValidator<GetAccountTransactionsRequest>> validators, 
                                             IAccountsTransactionsUnitOfWork accountsTransactionsUnitOfWork,
                                             IMapper mapper,
                                             ILogger<GetAccountTransactionsService> logger) 
            : base(validators, logger)
        {
            this.accountsTransactionsUnitOfWork = accountsTransactionsUnitOfWork;
            this.mapper = mapper;
        }

        protected override async Task<IEnumerable<GetAccountTransactionsResponse>> ExecuteRequest(BusinessServiceRequest<GetAccountTransactionsRequest> request, CancellationToken token = default)
        {
            var transactions = await accountsTransactionsUnitOfWork.AccountsTransactions.GetAccountTransactions(request.Request.AccountId)
                                                             .AsNoTracking().ToListAsync(token);

            return mapper.Map<IEnumerable<AccountTransaction>, IEnumerable<GetAccountTransactionsResponse>>(transactions);
        }
    }
}
