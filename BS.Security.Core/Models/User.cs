using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
