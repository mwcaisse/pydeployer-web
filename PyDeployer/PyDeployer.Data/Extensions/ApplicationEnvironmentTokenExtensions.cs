using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Extensions
{
    public static class ApplicationEnvironmentTokenExtensions
    {

        public static IQueryable<ApplicationEnvironmentToken> Build(this IQueryable<ApplicationEnvironmentToken> query)
        {
            return query
                .Include(aet => aet.ApplicationEnvironment)
                    .ThenInclude(ae => ae.Application)
                .Include(aet => aet.ApplicationEnvironment)
                    .ThenInclude(ae => ae.Environment)
                .Include(aet => aet.ApplicationToken)
                    .ThenInclude(at => at.Application);
        }
    }
}
