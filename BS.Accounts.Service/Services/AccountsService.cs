using AutoMapper;
using BS.Accounts.Core.Handlers;
using BS.Accounts.Core.Services;
using BS.Common.Core.Services;
using Google.Protobuf.Collections;
using Grpc.Core;
using DTO = BS.Accounts.Core.DTO;

namespace BS.Accounts.Service.Services
{
    public class AccountsService : Account.AccountBase
    {
        private readonly IAddAccountService addAccountService;
        private readonly IGetAccountService getAccountService;
        private readonly IGetAccountsService getAccountsService;
        private readonly IAccountBalanceChangedEventHandler accountBalanceChangedEventHandler;
        private readonly IMapper mapper;

        public AccountsService(IAddAccountService addAccountService,
                               IGetAccountService getAccountService,
                               IGetAccountsService getAccountsService,
                               IAccountBalanceChangedEventHandler accountBalanceChangedEventHandler,
                               IMapper mapper)
        {
            this.addAccountService = addAccountService;
            this.getAccountService = getAccountService;
            this.getAccountsService = getAccountsService;
            this.accountBalanceChangedEventHandler = accountBalanceChangedEventHandler;
            this.mapper = mapper;
        }

        public override async Task<AddAccountReply> AddAccount(AddAccountRequest request, ServerCallContext context)
        {
            var businessRequest = new BusinessServiceRequest<DTO.AddAccountRequest>(
                    mapper.Map<AddAccountRequest, DTO.AddAccountRequest>(request),
                    request.UserId);

            var result = await addAccountService.Execute(businessRequest, context.CancellationToken);

            if (result.Succeeded == false || result.Response != null)
            {
                var response = new AddAccountReply();
                response.Success = false;
                result.ValidationData.ToList().ForEach(v => response.Errors.Add(v.Message));
                return response;
            }
            else
            {
                var response = mapper.Map<DTO.AddAccountResponse, AddAccountReply>(result.Response);
                return response;
            }

        }

        public override async Task<GetAccountReply> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            var businessRequest = new BusinessServiceRequest<DTO.GetAccountRequest>(
                new DTO.GetAccountRequest() { AccountId = request.AccountId },
                request.UserId);

            var result = await getAccountService.Execute(businessRequest, context.CancellationToken);

            if (result.Response == null)
                return null;


            return mapper.Map<DTO.GetAccountResponse, GetAccountReply>(result.Response);
                            
        }

        public override async Task<GetAccountsReply> GetAccounts(GetAccountsRequest request, ServerCallContext context)
        {
            var businessRequest = new BusinessServiceRequest<DTO.GetAccountsRequest>(
                    new DTO.GetAccountsRequest(),
                    request.UserId
                );

            var result = await getAccountsService.Execute(businessRequest, context.CancellationToken);

    
            var response = new GetAccountsReply()
            {
                UserId = request.UserId,
            };

            if (result.Response != null)
                result.Response.ToList().ForEach(a => response.Accounts.Add(mapper.Map<DTO.GetAccountsResponse, GetAccountsReply.Types.GetAccountsReplyEntry>(a)));

            return response;

        }

        public override async Task<Empty> AccountBalanceChanged(AccountBalanceChangedRequest request, ServerCallContext context)
        {
            await accountBalanceChangedEventHandler.HandleEvent(new Core.Events.AccountBalanceChangedEvent()
            {
                AccountId = request.AccountId,
                Delta = (decimal)request.Delta
            }, context.CancellationToken);

            return new Empty();
        }
    }
}
