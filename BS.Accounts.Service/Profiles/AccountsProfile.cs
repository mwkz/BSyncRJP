using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DTO = BS.Accounts.Core.DTO;

namespace BS.Accounts.Service.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<AddAccountRequest, DTO.AddAccountRequest>();

            CreateMap<DTO.AddAccountResponse, AddAccountReply>()
                   .ForMember(t => t.UserId, opt => opt.Ignore())
                   .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedDate.ToUniversalTime())))
                   .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.UpdatedDate.ToUniversalTime())))
                   .ForMember(t => t.Errors, opt => opt.Ignore());

            CreateMap<DTO.GetAccountResponse, GetAccountReply>();

            CreateMap<DTO.GetAccountResponse, GetAccountReply>()
                   .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedDate.ToUniversalTime())))
                   .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.UpdatedDate.ToUniversalTime())));

            CreateMap<DTO.GetAccountsResponse, GetAccountsReply.Types.GetAccountsReplyEntry>()
                   .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedDate.ToUniversalTime())))
                   .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.UpdatedDate.ToUniversalTime())));
        }
    }
}
