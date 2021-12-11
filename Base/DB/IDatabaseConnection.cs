using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Base.DB
{
    /// <summary>
    /// Interface for a database connection.
    /// </summary>
    public interface IDatabaseConnection : IDisposable
    {
        /// <summary>
        /// Gets the underlying native database connection.
        /// </summary>
        DbConnection UnderlyingConnection { get; }

        /// <summary>
        /// Opens this database connection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task used to monitor the progress of the operation.</returns>
        Task OpenAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Setups this instance.
        /// </summary>
        /// <param name="databaseId">The database identifier.</param>
        void Setup(string databaseId);

        /// <summary>
        /// Creates the database command.
        /// </summary>
        /// <returns>
        /// The IDatabaseCommand instance.
        /// </returns>
        IDatabaseCommand CreateDatabaseCommand();

        /// <summary>
        /// Creates the database command receiving a command text.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        /// IDatabaseCommand with a command text associated.
        /// </returns>
        IDatabaseCommand CreateDatabaseCommand(string commandText);

    }
}
