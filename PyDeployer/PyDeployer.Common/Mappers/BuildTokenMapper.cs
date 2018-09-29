using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PyDeployer.Common.Entities;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Common.Mappers
{
    public static class BuildTokenMapper
    {
        public static BuildTokenViewModel ToViewModel(this BuildToken buildToken)
        {
            var vm = new BuildTokenViewModel()
            {
                BuildTokenId = buildToken.BuildTokenId,
                Name = buildToken.Name,
                Value = buildToken.Value
            };

            return vm;
        }

        public static IEnumerable<BuildTokenViewModel> ToViewModel(this IEnumerable<BuildToken> buildTokens)
        {
            return buildTokens.Select(bt => bt.ToViewModel());
        }
    }
}
