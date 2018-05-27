using System;
using System.Collections.Generic;
using System.Text;

namespace PyDeployer.Common.Entities
{
    public class ApplicationEnvironment
    {

        public long ApplicationEnvironmentId { get; set; }

        public long ApplicationId { get; set; }

        public long EnvironmentId { get; set; }

        public virtual Application Application { get; set; }

        public virtual Environment Environment { get; set; }

        public virtual ICollection<ApplicationEnvironmentToken> ApplicationEnvironmentTokens { get; set; }

    }
}
