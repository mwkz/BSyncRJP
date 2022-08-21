using AutoMapper;
using BS.Accounts.Service;
using VM = BS.APIGateway.ViewModels;

namespace BS.APIGateway.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<VM.NewAccount, AddAccountRequest>();
            CreateMap<GetAccountReply, VM.Account>()
                    .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => s.UpdatedDate.ToDateTime()))
                    .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToDateTime()));

            CreateMap<AddAccountReply, VM.Account>()
                    .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => s.UpdatedDate.ToDateTime()))
                    .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToDateTime()));

            CreateMap<GetAccountsReply.Types.GetAccountsReplyEntry, VM.Account>()
                    .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => s.UpdatedDate.ToDateTime()))
                    .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToDateTime()));

            CreateMap<VM.Account, VM.AccountSummary>();

        }
    }
}
