using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.DTO
{
    public class AddAccountTransactionRequest
    {
        public int AccountId { get; set; }

        public decimal Value { get; set; }

        public bool IsInitial { get; set; } = false;

    }
}
