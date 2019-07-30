using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckWithAspNetCoreDiagnostics
{
    public static class DependenciesExtensions
    {
        public static IHealthChecksBuilder AddDependecies(this IHealthChecksBuilder builder, List<Dependency> dependencies)
        {
            foreach (var itemDependencies in dependencies)
            {
                if (itemDependencies.Name.ToLower().StartsWith("sqlserver"))
                {
                    builder = builder.AddSqlServer(itemDependencies.ConnectionString, name: itemDependencies.Name);
                }
            }

            return builder;
        }
    }
}
