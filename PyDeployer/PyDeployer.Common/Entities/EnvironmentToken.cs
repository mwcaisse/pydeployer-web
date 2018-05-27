using System;
using System.Collections.Generic;
using System.Text;
using Mitchell.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class EnvironmentToken : ITrackedEntity, IActiveEntity
    {
        public long EnvironmentTokenId { get; set; }

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
