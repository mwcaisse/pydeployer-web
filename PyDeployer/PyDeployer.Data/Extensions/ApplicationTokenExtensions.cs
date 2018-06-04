using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Extensions
{
    public static class ApplicationTokenExtensions
    {

        public static IQueryable<ApplicationToken> Build(this IQueryable<ApplicationToken> query)
        {
            return query.Include(at => at.Application);
        }

    }
}
