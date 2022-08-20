using Grpc.Core;

namespace BS.Transactions.Service.Services
{
    public class AccountsTransactionsService : AccountsTransactions.AccountsTransactionsBase
    {
        public AccountsTransactionsService()
        {

        }

        public override Task<AddAccountTransactionResponse> AddAccountTransaction(AddAccountTransactionRequest request, ServerCallContext context)
        {
            return base.AddAccountTransaction(request, context);
        }

        public override Task<GetAccountTransactionsResponse> GetAccountTransactions(GetAccountTransactionsRequest request, ServerCallContext context)
        {
            return base.GetAccountTransactions(request, context);
        }
    }
}
