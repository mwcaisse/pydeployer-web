using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mitchell.Common.Converters;
using PyDeployer.Data;
using PyDeployer.Logic.Services;
using PyDeployer.Web.Converters;
using PyDeployer.Web.Middleware;

namespace PyDeployer.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("dbConfig.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddDbContext<PyDeployerDbContext>(options =>
                options.UseMySql(Configuration.GetSection("connectionString").Value)
            );

            services.AddTransient<ApplicationService>();
            services.AddTransient<ApplicationTokenService>();
            services.AddTransient<EnvironmentService>();
            services.AddTransient<ApplicationEnvironmentService>();
            services.AddTransient<ApplicationEnvironmentTokenService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                //View Model serializers for entities
                options.SerializerSettings.Converters.Add(new JsonApplicationConverter());
                options.SerializerSettings.Converters.Add(new JsonApplicationEnvironmentTokenConverter());
                options.SerializerSettings.Converters.Add(new JsonApplicationTokenConverter());
                options.SerializerSettings.Converters.Add(new JsonEnvironmentConverter());

                //General Serializers
                options.SerializerSettings.Converters.Add(new JsonDateEpochConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
