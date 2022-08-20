using AutoMapper;
using VM = BS.APIGateway.ViewModels;

namespace BS.APIGateway.Profiles
{
    public class AccountTransactionProfile : Profile
    {
        public AccountTransactionProfile()
        {
            CreateMap<VM.Account, BS.Accounts.Service.GetAccountReply>();
            CreateMap<VM.Account, BS.Accounts.Service.AddAccountReply>();

            CreateMap<VM.NewTransaction, BS.Transactions.Service.GetAccountTransactionsRequest>();

            

            CreateMap<VM.NewAccount, BS.Accounts.Service.AddAccountRequest>();

        }
    }
}
