using System;
using System.Collections.Generic;
using System.Text;
using Mitchell.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class ApplicationToken : ITrackedEntity, IActiveEntity
    {
        public long ApplicationTokenId { get; set; }

        public string Name { get; set; }

        public long ApplicationId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }

        public virtual Application Application { get; set; }

    }
}
