using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Configuration
{
    /// <summary>
    /// Options for the configuration of cross domain requests.
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// Gets or sets the allowed origins.
        /// </summary>
        /// <value>
        /// The allowed origins.
        /// </value>
        public string[] AllowedOrigins { get; set; }
    }
}
