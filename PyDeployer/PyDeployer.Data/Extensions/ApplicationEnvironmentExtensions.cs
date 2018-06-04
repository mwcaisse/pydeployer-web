using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Extensions
{
    public static class ApplicationEnvironmentExtensions
    {

        public static IQueryable<ApplicationEnvironment> Build(this IQueryable<ApplicationEnvironment> query)
        {
            return query.Include(ae => ae.Application).Include(ae => ae.Environment);
        }

    }
}
