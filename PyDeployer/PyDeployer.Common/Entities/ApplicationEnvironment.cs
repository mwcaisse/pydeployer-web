using System;
using System.Collections.Generic;
using System.Text;
using OwlTin.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class ApplicationEnvironment : IActiveEntity, ITrackedEntity
    {

        public long ApplicationEnvironmentId { get; set; }

        public long ApplicationId { get; set; }

        public long EnvironmentId { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Application Application { get; set; }

        public virtual Environment Environment { get; set; }

        public virtual ICollection<ApplicationEnvironmentToken> ApplicationEnvironmentTokens { get; set; }

    }
}
