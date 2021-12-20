using Base.DB;
using Base.Web;
using Framework.Configuration;
using Framework.Exceptions;
using Framework.Web.API.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Framework.Web.API
{
    public class Startup : BaseStartup
    {
        private ILogger<Startup> logger = null;
        public Startup(IWebHostEnvironment environment, IConfiguration configuration) : base()
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        //public IConfiguration Configuration { get; }

        //public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CorsOptions corsOptions = new CorsOptions();
            IConfigurationSection corsConfigSection = this.Configuration.GetSection(nameof(CorsOptions));
            corsConfigSection.Bind(corsOptions);
            services
              .AddCors(options => options.AddDefaultPolicy(builder =>
              {
                  builder.WithOrigins(corsOptions.AllowedOrigins)
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .SetPreflightMaxAge(TimeSpan.FromHours(10));
              }));

            this.SetupConfigurationDI(services);
            this.SetupDatabaseDbContext(services);
            this.SetupServicesDI(services);
            this.SetupExceptionManagement(services);

            services.AddControllers();

            //configure basic authentication
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            this.logger = logger;

            this.LogAllEnvironmentVariables(this.logger);
            this.LogAllConfiguration(this.Configuration, this.logger);

            if (this.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllers();
                endpoints.MapFallback((HttpContext context) =>
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotImplemented;
                    return Task.CompletedTask;
                });
            });
        }

        private void SetupConfigurationDI(IServiceCollection services)
        {
            services.AddLogging();

            services.Configure<BasicAuth>(this.Configuration.GetSection(nameof(BasicAuth)));
            services.Configure<DatabaseOptions>(this.Configuration.GetSection(nameof(DatabaseOptions)));
        }


        private void SetupDatabaseDbContext(IServiceCollection services)
        {
            DatabaseOptions databaseOptions = new DatabaseOptions();
            this.Configuration.GetSection(nameof(DatabaseOptions)).Bind(databaseOptions);

            var builder = new SqlConnectionStringBuilder(databaseOptions.ConnectionStrings["Default"]);
            if (databaseOptions.Passwords != null)
            {
                string password = databaseOptions.Passwords["Default"];

                if (!string.IsNullOrEmpty(password))
                {
                    builder.Password = password;
                }
            }


            services
              .AddDbContext<Repositories.ISharedDbContext, Repositories.SharedDbContext>(
                  options => options.UseSqlServer(builder.ConnectionString));
        }

        private void SetupServicesDI(IServiceCollection services)
        {
            new SetupServicesDI().ConfigureServices(services);
        }

        private void SetupExceptionManagement(IServiceCollection services)
        {
            ExceptionManagement exceptionManagement = new ExceptionManagement();

            services.AddSingleton<Base.Exceptions.IExceptionManagement>(exceptionManagement);
            services.AddSingleton<IExceptionManagement>(exceptionManagement);
        }

    }
}
