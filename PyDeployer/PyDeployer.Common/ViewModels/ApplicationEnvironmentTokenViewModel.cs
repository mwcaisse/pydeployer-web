using System;
using System.Collections.Generic;
using System.Text;

namespace PyDeployer.Common.ViewModels
{
    public class ApplicationEnvironmentTokenViewModel
    {

        public long ApplicationEnvironmentTokenId { get; set; }

        public long ApplicationId { get; set; }

        public long EnvironmentId { get; set; }

        public long ApplicationTokenId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

    }
}
