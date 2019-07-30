using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HealthCheckWithAspNetCoreDiagnostics
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dependencieData = new List<Dependency>();

            new ConfigureFromConfigurationOptions<List<Dependency>>(Configuration.GetSection("Dependencies")).Configure(dependencieData);
            dependencieData = dependencieData.OrderBy(x => x.Name).ToList();

            services.AddHealthChecks().AddDependecies(dependencieData);
            services.AddHealthChecksUI();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/healthchecks-data-ui", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });


            app.UseHealthChecksUI();
        }
    }
}
