using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Common.Mappers
{
    public static class ApplicationEnvironmentTokenMapper
    {

        public static ApplicationEnvironmentTokenViewModel ToViewModel(
            this ApplicationEnvironmentToken token)
        {
            var vm = new ApplicationEnvironmentTokenViewModel()
            {
                ApplicationEnvironmentTokenId = token.ApplicationEnvironmentTokenId,
                Name = token.ApplicationToken.Name,
                Value = token.Value,
                ApplicationId = token.ApplicationEnvironment.ApplicationId,
                EnvironmentId = token.ApplicationEnvironment.EnvironmentId
            };

            return vm;
        }

        public static IEnumerable<ApplicationEnvironmentTokenViewModel> ToViewModel(
            this IEnumerable<ApplicationEnvironmentToken> tokens)
        {
            return tokens.Select(t => t.ToViewModel());
        }

    }
}
