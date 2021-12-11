using System;
using System.Data;

namespace Base.Exceptions
{
    /// <summary>
    /// This interface represents all possible exceptions to be throw by the base libraries.
    /// </summary>
    public interface IExceptionManagement
    {
        /// <summary>
        /// Throws an Exception when a database type being used to add a parameter is not implemented.
        /// </summary>
        /// <param name="databaseType">Type of the database.</param>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseCommand_ParameterDatabaseTypeNotImplemented(DbType databaseType);

        /// <summary>
        /// Throws an Exception when the Action to process the data records of the ExecuteReader method is empty.
        /// </summary>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseCommand_DataRecordProcessActionArgumentEmpty();

        /// <summary>
        /// Throws an Exception when the Database type being inferred is not supported.
        /// </summary>
        /// <param name="innerType">Type of the inner.</param>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseCommand_DatabaseTypeNotSupported(Type innerType);

        /// <summary>
        /// Throws an Exception when the Database command detects that a query is being executed under an aborted transaction scope.
        /// </summary>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseCommand_AbortedTransactionDetected();

        /// <summary>
        /// Throws an Exception when a nested database connection is detected.
        /// </summary>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseConnection_NestedConnectionDetected();

        /// <summary>
        /// Throws an Exception when a transaction nested database connection is detected.
        /// </summary>
        /// <returns>The exception to be thrown.</returns>
        Exception Throw_DbDatabaseConnection_TransactionNestedConnectionDetected();

        /// <summary>
        /// Throw an Invalid Operation Exception when the flyweight factory factory is already closed.
        /// </summary>
        /// <param name="typeOfEntity">The type of entity.</param>
        /// <returns>The exception to be thrown.</returns>
        /// <exception cref="System.InvalidOperationException">Flyweight factory is closed.</exception>
        Exception Throw_FlyweightFactory_FactoryIsClosed(Type typeOfEntity);

        
    }
}
