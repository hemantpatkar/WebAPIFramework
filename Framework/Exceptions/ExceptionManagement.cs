using System;
using System.Data;
using System.Globalization;

namespace Framework.Exceptions
{
    /// <summary>
    /// This class represents the managements of exception throwing.
    /// </summary>
    public class ExceptionManagement : IExceptionManagement, Base.Exceptions.IExceptionManagement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionManagement"/> class.
        /// </summary>
        public ExceptionManagement()
        {
        }

        #region Base.Exceptions.IExceptionManagement

        /// <summary>
        /// Throws a Not Implemented Exception when a database type being used to add a parameter is not implemented.
        /// </summary>
        /// <param name="databaseType">Type of the database.</param>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        public Exception Throw_DbDatabaseCommand_ParameterDatabaseTypeNotImplemented(DbType databaseType)
        {
            return new NotImplementedException("Database Type " + databaseType.ToString() + " it's not implemented");
        }

        /// <summary>
        /// Throws an Argument Exception when the Action to process the data records of the ExecuteReader method is empty.
        /// </summary>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        public Exception Throw_DbDatabaseCommand_DataRecordProcessActionArgumentEmpty()
        {
            return new ArgumentException("The argument cannot be empty");
        }

        /// <summary>
        /// Throws a Not Supported Exception when the Database type being inferred is not supported.
        /// </summary>
        /// <param name="innerType">The database inner type.</param>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Thrown when the native type cannot be inferred.</exception>
        public Exception Throw_DbDatabaseCommand_DatabaseTypeNotSupported(Type innerType)
        {
            return new NotSupportedException("Cannot infer native database type for type : " + innerType.FullName);
        }

        /// <summary>
        /// Throws an Argument Exception when the Database command detects that a query is being executed under an aborted transaction scope.
        /// </summary>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        public Exception Throw_DbDatabaseCommand_AbortedTransactionDetected()
        {
            return new ArgumentException("Cannot execute query after transaction abort.");
        }

        /// <summary>
        /// Throws an Invalid Operation Exception when a nested database connection is detected.
        /// </summary>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        public Exception Throw_DbDatabaseConnection_NestedConnectionDetected()
        {
            return new InvalidOperationException("Nested DB Connection");
        }

        /// <summary>
        /// Throws an Invalid Operation Exception when a transaction nested database connection is detected.
        /// </summary>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        public Exception Throw_DbDatabaseConnection_TransactionNestedConnectionDetected()
        {
            return new InvalidOperationException("Transaction Nested DB Connection");
        }

        #endregion

        /// <summary>
        /// Throw an Invalid Operation Exception when the flyweight factory factory is already closed.
        /// </summary>
        /// <param name="typeOfEntity">The type of entity.</param>
        /// <returns>
        /// The exception to be thrown.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Flyweight factory is closed.</exception>
        public Exception Throw_FlyweightFactory_FactoryIsClosed(Type typeOfEntity)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Flyweight factory for entity type '{0}' is closed.", typeOfEntity.Name));
        }

       
    }
}
