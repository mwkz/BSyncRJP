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

            CreateMap<Transactions.Service.GetAccountTransactionsResponse, VM.AccountTransaction>();


            CreateMap<Transactions.Service.AddAccountTransactionResponse, VM.AccountTransaction>()
                    .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => s.UpdatedDate.ToDateTime()))
                    .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToDateTime()));

            CreateMap<VM.NewTransaction, Transactions.Service.AddAccountTransactionRequest>();

            CreateMap<VM.NewAccount, BS.Accounts.Service.AddAccountRequest>();

            CreateMap<Transactions.Service.GetAccountTransactionsResponse.Types.GetAccountTransactionsResponseEntry, VM.AccountTransaction>()
                    .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => s.UpdatedDate.ToDateTime()))
                    .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToDateTime()));




        }
    }
}
