using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OwlTin.Authentication;
using OwlTin.Authentication.Data;
using OwlTin.Authentication.Managers;
using OwlTin.Authentication.Middleware;
using OwlTin.Authentication.Models;
using OwlTin.Authentication.PasswordHasher;
using OwlTin.Authentication.Services;
using OwlTin.Common.Converters;
using PyDeployer.Data;
using PyDeployer.Logic.Services;
using PyDeployer.Web.Configuration;
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
                .AddJsonFile("dbConfig.json")
                .AddJsonFile("deploymentConfig.json");

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
            //Alias the DbContext to its interface
            services.AddScoped<IAuthenticationDbContext>(provider => provider.GetService<PyDeployerDbContext>());

            var rootPathPrefix = Configuration.GetValue<string>("rootPathPrefix", "");

            var applicationConfig = new ApplicationConfiguration()
            {
                RootPathPrefix = rootPathPrefix
            };
            services.AddSingleton(applicationConfig);

            //Authentication Services
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddTokenAuthentication(options => { })
                .AddCookie(options =>
                {
                    options.LoginPath = applicationConfig.PrefixUrl("/login");
                    options.LogoutPath = applicationConfig.PrefixUrl("/logout");
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        if (context.Request.Path.Value.Contains("/api/"))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            context.Response.Redirect(options.LoginPath);
                        }
                        return Task.CompletedTask;
                    };
                });

            //Add HttpContextAccessor as a Service
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ApplicationService>();
            services.AddTransient<ApplicationTokenService>();
            services.AddTransient<EnvironmentService>();
            services.AddTransient<ApplicationEnvironmentService>();
            services.AddTransient<ApplicationEnvironmentTokenService>();

            // Authentication Stuff
            services.AddTransient<IRegistrationKeyService, RegistrationKeyService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserAuthenticationTokenService, UserAuthenticationTokenService>();
            services.AddTransient<IPasswordHasher, ArgonPasswordHasher>();

            services.AddTransient<UserAuthenticationManager>();
            services.AddSingleton<SessionTokenManager>();

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

            services.AddScoped<IRequestInformation, ServerRequestInformation>();
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

            app.UseAuthentication();
            app.UseTokenAuthentication();

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
