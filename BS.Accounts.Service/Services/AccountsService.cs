using Grpc.Core;

namespace BS.Accounts.Service.Services
{
    public class AccountsService  : BS.Accounts.Service.Account.AccountBase
    {
        public AccountsService()
        {

        }

        public override Task<AddAccountReply> AddAccount(AddAccountRequest request, ServerCallContext context)
        {
            return base.AddAccount(request, context);
        }

        public override Task<GetAccountReply> GetAccount(GetAccountRequest request, ServerCallContext context)
        {
            return base.GetAccount(request, context);
        }

        public override Task<GetAccountsReply> GetAccounts(GetAccountsRequest request, ServerCallContext context)
        {                        
            return base.GetAccounts(request, context);
        }
    }
}
