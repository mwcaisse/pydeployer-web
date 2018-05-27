using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Common.Mappers
{
    public static class EnvironmentMapper
    {

        public static EnvironmentViewModel ToViewModel(this Entities.Environment environment)
        {
            var vm = new EnvironmentViewModel()
            {
                EnvironmentId = environment.EnvironmentId,
                EnvironmentUuid = environment.EnvironmentUuid,
                Name = environment.Name,
                HostName = environment.HostName
            };

            return vm;
        }

        public static IEnumerable<EnvironmentViewModel> ToViewModel(
            this IEnumerable<Entities.Environment> environments)
        {
            return environments.Select(e => e.ToViewModel());
        }

    }
}
