﻿using System;
using System.Collections.Generic;
using System.Text;
using Mitchell.Common.Entities;

namespace Mitchell.Authentication.Entities
{
    public partial class UserRegistrationKeyUse : ITrackedEntity
    {
        public long UserRegistrationKeyUseId { get; set; }

        public long UserRegistrationKeyId { get; set; }
        public long UserId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual UserRegistrationKey UserRegistrationKey { get; set; }
        public virtual User User { get; set; }
    }
}
