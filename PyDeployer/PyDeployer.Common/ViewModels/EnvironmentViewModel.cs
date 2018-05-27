using System;
using System.Collections.Generic;
using System.Text;

namespace PyDeployer.Common.ViewModels
{
    public class EnvironmentViewModel
    {

        public long EnvironmentId { get; set; }

        public Guid EnvironmentUuid { get; set; }

        public string Name { get; set; }

        public string HostName { get; set; }
    }
}
