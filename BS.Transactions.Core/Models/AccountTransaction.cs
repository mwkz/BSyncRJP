using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Models
{
    public class AccountTransaction
    {
        public long Id { get; set; }

        public int AccountId { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedByUserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public AccountBalance AccountBalance { get; set; }

        

    }
}
