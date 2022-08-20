using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Models
{
    public class AccountBalance
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public ICollection<AccountTransaction> Transactions { get; set; }
    }
}
