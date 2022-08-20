using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Core.DTO
{
    public class AuthenticateUserResponse
    {
        public bool Authenticated { get; set; }

        public int? Id { get; set; }

        public string? Username { get; set; }
    }
}
