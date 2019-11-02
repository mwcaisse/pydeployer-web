using System;
using System.Collections.Generic;
using System.Text;
using OwlTin.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class Environment : ITrackedEntity, IActiveEntity
    {
        public long EnvironmentId { get; set; }

        public Guid EnvironmentUuid { get; set; }

        public string Name { get; set; }

        public string HostName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }

        public ICollection<ApplicationEnvironment> ApplicationEnvironments { get; set; }
        
        public ICollection<Database> Databases { get; set; }

    }
}
