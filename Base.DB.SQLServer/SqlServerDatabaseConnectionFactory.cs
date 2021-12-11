using Microsoft.Extensions.Options;

namespace Base.DB.SQLServer
{
    /// <summary>
    /// The SQLServer database connection factory, responsible for creating physical database connections to an SQL server.
    /// </summary>
    public class SqlServerDatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        /// <summary>
        /// Databases the profiler.
        /// </summary>
        private readonly IDatabaseProfiler databaseProfiler;

        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptionsMonitor<DatabaseOptions> databaseOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDatabaseConnectionFactory" /> class.
        /// </summary>
        /// <param name="databaseProfiler">The database profiler.</param>
        /// <param name="databaseOptions">The database options.</param>
        public SqlServerDatabaseConnectionFactory(
            IDatabaseProfiler databaseProfiler,
            IOptionsMonitor<DatabaseOptions> databaseOptions)
        {
            this.databaseProfiler = databaseProfiler;
            this.databaseOptions = databaseOptions;
        }

        /// <summary>
        /// Creates a database connection for the default database.
        /// </summary>
        /// <returns>
        /// The database connection.
        /// </returns>
        public IDatabaseConnection Create()
        {
            return this.Create("Default");
        }

        /// <summary>
        /// Creates a database connection with the specified database id.
        /// </summary>
        /// <param name="databaseId">The database id.</param>
        /// <returns>
        /// The database connection.
        /// </returns>
        public IDatabaseConnection Create(string databaseId)
        {
            IDatabaseConnection connection = new SqlServerDatabaseConnection(
                this.databaseProfiler,
                this.databaseOptions);

            connection.Setup(databaseId);

            return connection;
        }
    }
}
