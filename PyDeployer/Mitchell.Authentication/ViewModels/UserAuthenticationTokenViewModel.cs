﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mitchell.Authentication.ViewModels
{
    public class UserAuthenticationTokenViewModel
    {
        public long UserAuthenticationTokenId { get; set; }
        public long UserId { get; set; }
        public string DeviceUuid { get; set; }
        public bool Active { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastLoginAddress { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
