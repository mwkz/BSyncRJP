using AutoMapper;
using BS.Accounts.Core.Clients;
using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Models;
using BS.Accounts.Core.Repositories;
using BS.Accounts.Core.Services;
using BS.Common.Core.Services;
using BS.Common.Infrastructure.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Services
{
    public class AddAccountService : BusinessService<AddAccountRequest, AddAccountResponse>, IAddAccountService
    {
        private readonly IAccountsUnitOfWork accountsUnitOfWork;
        private readonly IAccountsTransactionsServiceClient transactionsServiceClient;
        private readonly IMapper mapper;

        public AddAccountService(IEnumerable<IValidator<AddAccountRequest>> validators,
                                    IAccountsUnitOfWork accountsUnitOfWork,
                                    IAccountsTransactionsServiceClient transactionsServiceClient,
                                    IMapper mapper,
                                    ILogger<AddAccountService> logger) 
            : base(validators, logger)
        {
            this.accountsUnitOfWork = accountsUnitOfWork;
            this.transactionsServiceClient = transactionsServiceClient;
            this.mapper = mapper;
        }

        protected override async Task<AddAccountResponse> ExecuteRequest(BusinessServiceRequest<AddAccountRequest> request, CancellationToken token = default)
        {
            using(var transaction = await accountsUnitOfWork.BeginTransaction(token))
            {
                var account = mapper.Map<AddAccountRequest, Account>(request.Request);
                
                account.UpdatedByUserId = request.UserId;
                account.CreatedByUserId = request.UserId;
                account.UpdatedDate = DateTime.UtcNow;
                account.CreatedDate = DateTime.UtcNow;

                accountsUnitOfWork.Accounts.Add(account);
                await accountsUnitOfWork.SaveChanges(token);


                //This should be fire and foreget
                await transactionsServiceClient.NotifyAccountAdded(account.Id, request.Request.InitialBalance, request.UserId, token);

                await transaction.Complete(token);
                return mapper.Map<Account, AddAccountResponse>(account);                
                
            }
        }
    }
}
