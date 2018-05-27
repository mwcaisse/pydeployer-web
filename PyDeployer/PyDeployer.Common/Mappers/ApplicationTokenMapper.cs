using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Common.Mappers
{
    public static class ApplicationTokenMapper
    {

        public static ApplicationTokenViewModel ToViewModel(this ApplicationToken token)
        {
            var vm = new ApplicationTokenViewModel()
            {
                ApplicationId = token.ApplicationId,
                ApplicationTokenId = token.ApplicationTokenId,
                Name = token.Name
            };

            return vm;
        }

        public static IEnumerable<ApplicationTokenViewModel> ToViewModel(this IEnumerable<ApplicationToken> tokens)
        {
            return tokens.Select(t => t.ToViewModel());
        }

    }
}
