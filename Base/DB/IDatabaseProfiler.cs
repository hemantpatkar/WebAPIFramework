using System;
using System.Data.Common;

namespace Base.DB
{

    /// <summary>
    /// Interface for a database profiler.
    /// </summary>
    public interface IDatabaseProfiler
    {
        /// <summary>
        /// Gets a value indicating whether the profiling is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the profiling is active; otherwise, <c>false</c>.
        /// </value>
        bool IsActive { get; }

        /// <summary>
        /// Called when a reader finishes executing.
        /// </summary>
        /// <param name="databaseCommand">The database command.</param>
        /// <param name="executeType">Type command being executed.</param>
        /// <param name="reader">The database reader.</param>
        void ExecuteFinish(DbCommand databaseCommand, DatabaseProfilerExecuteType executeType, DbDataReader reader);

        /// <summary>
        /// Called when a command starts executing.
        /// </summary>
        /// <param name="databaseCommand">The database command.</param>
        /// <param name="executeType">Type command being executed.</param>
        void ExecuteStart(DbCommand databaseCommand, DatabaseProfilerExecuteType executeType);

        /// <summary>
        /// Called when an error happens during execution of a command.
        /// </summary>
        /// <param name="databaseCommand">The database command.</param>
        /// <param name="executeType">Type command being executed.</param>
        /// <param name="exception">The exception that caused an error.</param>
        void OnError(DbCommand databaseCommand, DatabaseProfilerExecuteType executeType, Exception exception);

        /// <summary>
        /// Called when a reader is done iterating through the data.
        /// </summary>
        /// <param name="reader">The database reader.</param>
        void ReaderFinish(DbDataReader reader);
    }
}
