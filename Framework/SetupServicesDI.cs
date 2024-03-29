﻿using Framework.Repositories;
using Framework.Services;
using Framework.Services.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Framework
{
    public class SetupServicesDI
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGenericService, GenericService>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddSingleton<ILogWriter, LogWriter>();
        }
    }
}
