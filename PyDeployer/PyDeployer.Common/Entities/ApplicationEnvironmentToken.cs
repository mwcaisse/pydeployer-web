using System;
using System.Collections.Generic;
using System.Text;
using OwlTin.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class ApplicationEnvironmentToken : ITrackedEntity, IActiveEntity
    {
        public long ApplicationEnvironmentTokenId { get; set; }

        public long ApplicationEnvironmentId { get; set; }

        public long ApplicationTokenId { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }

        public virtual ApplicationEnvironment ApplicationEnvironment { get; set; }

        public virtual ApplicationToken ApplicationToken { get; set; }
    }
}
