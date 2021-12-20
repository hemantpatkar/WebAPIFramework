using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections;
using System.Text;

namespace Base.Web
{
    /// <summary>
    /// Abstract class for micro services startup that can be used in a monolithic style web application.
    /// </summary>
    public abstract class BaseStartup
    {
        /// <summary>
        /// Gets or sets the information about the web hosting environment an application is running in.
        /// </summary>
        public IWebHostEnvironment Environment { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Logs all environment variables.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected void LogAllEnvironmentVariables(ILogger logger)
        {
            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine("========= EnvVar =========");

            foreach (DictionaryEntry item in System.Environment.GetEnvironmentVariables())
            {
                if (this.IsConfidentialValue(item.Key.ToString()))
                {
                    buffer.AppendLine($"Key:{item.Key} Value:<<hidden>>");
                }
                else
                {
                    buffer.AppendLine($"Key:{item.Key} Value:{item.Value}");
                }
            }

            buffer.AppendLine("=========================");

            logger.LogInformation(buffer.ToString());
        }

        /// <summary>
        /// Logs all configuration.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="logger">The logger.</param>
        protected void LogAllConfiguration(IConfiguration item, ILogger logger)
        {
            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine("========= Config =========");

            if (item != null)
            {
                foreach (var item2 in item.GetChildren())
                {
                    this.LogAllConfigurationSection(item2, buffer);
                }
            }

            buffer.AppendLine("=========================");

            logger.LogInformation(buffer.ToString());
        }

        /// <summary>
        /// Logs all configuration section.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="buffer">The buffer.</param>
        private void LogAllConfigurationSection(IConfigurationSection item, StringBuilder buffer)
        {
            if (this.IsConfidentialValue(item.Path))
            {
                buffer.AppendLine($"Path:{item.Path} Value:<<hidden>>");
            }
            else
            {
                buffer.AppendLine($"Path:{item.Path} Value:{item.Value}");
            }

            foreach (var item2 in item.GetChildren())
            {
                this.LogAllConfigurationSection(item2, buffer);
            }
        }

        private bool IsConfidentialValue(string key) =>
            !string.IsNullOrEmpty(key) && (key.Contains("password", StringComparison.InvariantCultureIgnoreCase) ||
                key.Contains("secret", StringComparison.InvariantCultureIgnoreCase) ||
                key.Contains("confidential", StringComparison.InvariantCultureIgnoreCase));
    }
}
