using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Events
{
    public class AccountBalanceChangedEvent
    {
        public int AccountId { get; set; }
        public decimal Delta { get; set; }
    }
}
