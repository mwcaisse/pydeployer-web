using System;
using System.Collections.Generic;
using System.Text;
using OwlTin.Common.Entities;

namespace PyDeployer.Common.Entities
{
    public class BuildToken : IActiveEntity, ITrackedEntity
    {
        public long BuildTokenId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Active { get; set; }
    }
}
