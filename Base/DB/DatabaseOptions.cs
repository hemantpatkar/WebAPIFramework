using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DB
{
    /// <summary>
    /// Exposes various settings needed to configure the connection to the database server.
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>
        /// Gets or sets the list of database connection strings.
        /// </summary>
        public IDictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the list of database connection passwords.
        /// </summary>
        public IDictionary<string, string> Passwords { get; set; }

        /// <summary>
        /// Gets or sets the current database provider.
        /// </summary>
        public DatabaseProviderValue CurrentProvider { get; set; }
    }
}
