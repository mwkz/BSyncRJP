using AutoMapper;
using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, GetAccountResponse>();                
            CreateMap<Account, GetAccountsResponse>();
            
            CreateMap<Account, AddAccountResponse>();
            CreateMap<AddAccountRequest, Account>();

        }
    }
}
