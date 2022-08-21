using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DTO = BS.Transactions.Core.DTO;

namespace BS.Transactions.Service.Profiles
{
    public class AccountTransactionProfile : Profile
    {
        public AccountTransactionProfile()
        {

            CreateMap<DTO.AddAccountTransactionResponse, AddAccountTransactionResponse>()
                   .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedDate.ToUniversalTime())))
                   .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.UpdatedDate.ToUniversalTime())));

            CreateMap<DTO.GetAccountTransactionsResponse, GetAccountTransactionsResponse.Types.GetAccountTransactionsResponseEntry>()
                   .ForMember(t => t.CreatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedDate.ToUniversalTime())))
                   .ForMember(t => t.UpdatedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.UpdatedDate.ToUniversalTime())));

        }
    }
}
