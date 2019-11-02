using System;
using System.Collections.Generic;
using System.Text;
using OwlTin.Common.Entities;
using PyDeployer.Common.Encryption;

namespace PyDeployer.Common.Entities
{
    public class Application : ITrackedEntity, IActiveEntity, IEncryptionContextEntity
    {

        public long ApplicationId { get; set; }

        public string Name { get; set; }

        public Guid ApplicationUuid { get; set; }

        public string EncryptionKey { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<ApplicationEnvironment> ApplicationEnvironments { get; set; }

        public virtual ICollection<ApplicationToken> ApplicationTokens { get; set; }

    }
}
