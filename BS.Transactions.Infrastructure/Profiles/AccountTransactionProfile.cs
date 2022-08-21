using AutoMapper;
using BS.Transactions.Core.DTO;
using BS.Transactions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Profiles
{
    public class AccountTransactionProfile : Profile
    {
        public AccountTransactionProfile()
        {
            CreateMap<AccountTransaction, AddAccountTransactionResponse>();
            CreateMap<AccountTransaction, GetAccountTransactionsResponse>();
            CreateMap<AddAccountTransactionRequest, AccountTransaction>();
        }
    }
}
