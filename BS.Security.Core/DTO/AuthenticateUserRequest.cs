﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Security.Core.DTO
{
    public class AuthenticateUserRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
