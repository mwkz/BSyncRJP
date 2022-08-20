using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Core.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string? AccountNo { get; set; }

        public string? Name { get; set; }
        
        public decimal Balance { get; set; }
        
        public int CreatedByUserId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public int UpdatedByUserId { get; set; }
        
        public DateTime UpdatedDate { get; set; }

        public int CustomerId { get; set; }

    }
}
