using System.Linq;
using Microsoft.EntityFrameworkCore;
using PyDeployer.Common.Entities;

namespace PyDeployer.Data.Extensions
{
    public static class DatabaseExtensions
    {

        public static IQueryable<Database> Build(this IQueryable<Database> query)
        {
            return query.Include(d => d.Environment);
        } 
        
    }
}