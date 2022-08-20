using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.DTO
{
    public class AddAccountRequest
    {
        public string? AccountNo { get; set; }

        public string? Name { get; set; }

        public decimal InitialBalance { get; set; }

        public int CustomerId { get; set; }
    }
}
