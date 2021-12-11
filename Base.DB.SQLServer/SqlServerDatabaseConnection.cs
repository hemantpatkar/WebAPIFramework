using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Base.DB.SQLServer
{
    /// <summary>
    /// Represents a open connection to an SqlServer database.
    /// </summary>
    public class SqlServerDatabaseConnection : IDatabaseConnection
    {
        /// <summary>
        /// The database profiler.
        /// </summary>
        private readonly IDatabaseProfiler databaseProfiler;

        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptionsMonitor<DatabaseOptions> databaseOptions;

        /// <summary>
        /// The SQL connection.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDatabaseConnection" /> class.
        /// </summary>
        /// <param name="databaseProfiler">The database profiler.</param>
        /// <param name="databaseOptions">The database options.</param>
        public SqlServerDatabaseConnection(
            IDatabaseProfiler databaseProfiler,
            IOptionsMonitor<DatabaseOptions> databaseOptions)
        {
            this.databaseProfiler = databaseProfiler;
            this.databaseOptions = databaseOptions;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlServerDatabaseConnection"/> class.
        /// </summary>
        ~SqlServerDatabaseConnection()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public DbConnection UnderlyingConnection => this.connection;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        /// <param name="databaseId">The database identifier.</param>
        public void Setup(string databaseId)
        {
            var databaseOptions = this.databaseOptions.CurrentValue;

            var builder = new SqlConnectionStringBuilder(databaseOptions.ConnectionStrings[databaseId]);

            if (databaseOptions.Passwords != null)
            {
                string password = databaseOptions.Passwords[databaseId];

                if (!string.IsNullOrEmpty(password))
                {
                    builder.Password = password;
                }
            }

            this.connection = new SqlConnection(builder.ConnectionString);
        }

        /// <summary>
        /// Opens this database connection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> that can be used to monitor the progress of the operation.</returns>
        public async Task OpenAsync(CancellationToken cancellationToken)
        {
            await this.connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates an database PL/SQL command.
        /// </summary>
        /// <returns>
        /// Database PL/SQL command.
        /// </returns>
        public IDatabaseCommand CreateDatabaseCommand()
        {
            return this.CreateDatabaseCommand(string.Empty);
        }

        /// <summary>
        /// Creates an database PL/SQL command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        /// Database PL/SQL command.
        /// </returns>
        public IDatabaseCommand CreateDatabaseCommand(string commandText)
        {
            SqlServerDatabaseCommand databaseCommand = new SqlServerDatabaseCommand(this.databaseProfiler);

            SqlCommand command = this.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = commandText;

            databaseCommand.Setup(command);

            return databaseCommand;
        }


        #region "IDisposable"

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up all managed resources
                if (this.connection != null)
                {
                    this.connection.Dispose();
                }
            }

            // Clean up all native resources
        }

        #endregion
    }
}
