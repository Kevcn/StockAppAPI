﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.V1.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
