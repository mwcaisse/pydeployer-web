using System;
using System.Collections.Generic;
using System.Text;
using Mitchell.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class Application : ITrackedEntity, IActiveEntity
    {

        public long ApplicationId { get; set; }

        public string Name { get; set; }

        public Guid ApplicationUuid { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }

        public virtual ApplicationEnvironment ApplicationEnvironment { get; set; }

        public virtual ICollection<ApplicationToken> ApplicationTokens { get; set; }

    }
}
