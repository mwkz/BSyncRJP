using AutoMapper;
using BS.Common.Core.Services;
using BS.Transactions.Core.Events;
using BS.Transactions.Core.Handlers;
using BS.Transactions.Core.Services;
using Grpc.Core;
using DTO = BS.Transactions.Core.DTO;

namespace BS.Transactions.Service.Services
{
    public class AccountsTransactionsService : AccountsTransactions.AccountsTransactionsBase
    {
        private readonly IAddAccountTransactionService addAccountTransactionService;
        private readonly IGetAccountTransactionsService getAccountTransactionsService;
        private readonly INewAccountAddedEventHandler newAccountAddedEventHandler;
        private readonly IMapper mapper;

        public AccountsTransactionsService(IAddAccountTransactionService addAccountTransactionService, 
                                           IGetAccountTransactionsService getAccountTransactionsService, 
                                           INewAccountAddedEventHandler newAccountAddedEventHandler,
                                           IMapper mapper)
        {
            this.addAccountTransactionService = addAccountTransactionService;
            this.getAccountTransactionsService = getAccountTransactionsService;
            this.newAccountAddedEventHandler = newAccountAddedEventHandler;
            this.mapper = mapper;
        }

        public override async Task<AddAccountTransactionResponse> AddAccountTransaction(AddAccountTransactionRequest request, ServerCallContext context)
        {
            var businessRequest = new BusinessServiceRequest<DTO.AddAccountTransactionRequest>
                (
                    new DTO.AddAccountTransactionRequest() {  AccountId =request.AccountId, IsInitial = false, Value = (decimal)request.Value },
                    request.UserId
                );

            var result = await addAccountTransactionService.Execute(businessRequest, context.CancellationToken);

            if (result.Succeeded && result.Response != null)
            {
                var response = mapper.Map<DTO.AddAccountTransactionResponse, AddAccountTransactionResponse>(result.Response);
                response.Success = true;
                response.UserId = request.UserId;
                return response;
            }
            else
            {
                var response = new AddAccountTransactionResponse();
                response.UserId = request.UserId;
                result.ValidationData.ToList().ForEach(v => response.Errors.Add(v.Message));
                return response;
            }
        }

        public override async Task<GetAccountTransactionsResponse> GetAccountTransactions(GetAccountTransactionsRequest request, ServerCallContext context)
        {
            var businessRequest = new BusinessServiceRequest<DTO.GetAccountTransactionsRequest>
                (
                    new DTO.GetAccountTransactionsRequest() {  AccountId = request.AccountId },
                    request.UserId
                );

            var result = await getAccountTransactionsService.Execute(businessRequest, context.CancellationToken);

            var value = new GetAccountTransactionsResponse();

            value.UserId = request.UserId;

            if (result != null && result.Response != null)
            {
                foreach (var transaction in result.Response)
                    value.Transactions.Add(mapper.Map<DTO.GetAccountTransactionsResponse, GetAccountTransactionsResponse.Types.GetAccountTransactionsResponseEntry>(transaction));
            }

             return value;
        }

        public override async Task<Empty> NewAccountAdded(NewAccountAddedRequest request, ServerCallContext context)
        {
            await newAccountAddedEventHandler.HandleEvent(new NewAccountAddedEvent()
            {
                AccountId = request.AccountId,
                InitialBalance = (decimal)request.InitialBalance,
                UserId = 1
            }, context.CancellationToken);

            return new Empty();
        }
    }
}
