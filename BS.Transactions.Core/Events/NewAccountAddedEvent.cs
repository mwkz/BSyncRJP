using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Core.Events
{
    public class NewAccountAddedEvent
    {
        public int AccountId { get; set; }
        public decimal InitialBalance { get; set; }
        public int UserId { get; set; }
    }
}
