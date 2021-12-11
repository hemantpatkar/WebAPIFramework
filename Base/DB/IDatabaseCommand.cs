using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Base.DB
{
    /// <summary>
    /// Interface for a database command.
    /// </summary>
    public interface IDatabaseCommand : IDisposable
    {
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        CommandType CommandType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>The command text.</value>
        string CommandText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value>The command timeout.</value>
        int CommandTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the initial size of the LOB fetch.
        /// </summary>
        /// <value>The initial size of the LOB fetch.</value>
        int InitialLOBFetchSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fetch size.
        /// </summary>
        /// <value>The fetch size.</value>
        long FetchSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the initial LOB fetch size config.
        /// </summary>
        /// <value>The initial LOB fetch size config.</value>
        int? InitialLOBFetchSizeConfig
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the command's parameter collection.
        /// </summary>
        /// <value>The parameters.</value>
        DbParameterCollection Parameters
        {
            get;
        }

        /// <summary>
        /// Adds the output parameter.
        /// </summary>
        /// <typeparam name="T">Object Type.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterOut<T>(string parameterName);

        /// <summary>
        /// Adds the parameter in / out.
        /// </summary>
        /// <typeparam name="T">Object Type.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        DbParameter AddParameterInOut<T>(string parameterName, T value);

        /// <summary>
        /// Create a database parameter with direction Out and a specific size.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="precision">The precision of parameter.</param>
        /// <param name="scale">The scale of parameter.</param>
        /// <returns>
        /// A database parameter with direction Out and a specific size.
        /// </returns>
        DbParameter AddParameterOut<T>(string parameterName, byte precision, byte scale);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="T">Type of the Parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameter<T>(string parameterName);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="T">Type of the Parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameter<T>(string parameterName, T value);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameter(Type type, string parameterName, object value);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="databaseTypeName">Name of the database type.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        DbParameter AddParameter(Type type, string parameterName, object value, string databaseTypeName);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="databaseTypeName">Name of the database type.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        DbParameter AddParameter(string typeFullName, string parameterName, object value, string databaseTypeName);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="databaseType">Type of the database.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        DbParameter AddParameter(DbType databaseType, string parameterName, object value);

        /// <summary>
        /// Adds the parameter of type NCLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterNCLOB(string parameterName, string value);

        /// <summary>
        /// Adds an output parameter of type NCLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        DbParameter AddParameterOutNCLOB(string parameterName);

        /// <summary>
        /// Adds the parameter of type CLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The string value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterCLOB(string parameterName, string value);

        /// <summary>
        /// Adds an output parameter of type CLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterOutCLOB(string parameterName);

        /// <summary>
        /// Adds the parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The Stream value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterBLOB(string parameterName, Stream value);

        /// <summary>
        /// Adds the parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The byte[] value.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterBLOB(string parameterName, byte[] value);

        /// <summary>
        /// Adds the parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterBLOB(string parameterName);

        /// <summary>
        /// Adds the parameter of type RefCursor.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        DbParameter AddParameterRefCursor(string parameterName);

        /// <summary>
        /// Executes the reader to retrieve only one record.
        /// </summary>
        /// <param name="processDataRecord">The data record processor.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// The number of fetched records.
        /// </returns>
        Task<bool> ExecuteReaderSingle(Action<IDataRecord> processDataRecord, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The data record processor.</param>
        /// <returns>
        /// The number of fetched records.
        /// </returns>
        Task<int> ExecuteReader(CancellationToken cancellationToken, params Action<IDataRecord>[] processDataRecord);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecords">The data record processor.</param>
        /// <returns>
        /// The number of fetched records.
        /// </returns>
        Task<int> ExecuteReader(CancellationToken cancellationToken, params DataRecordProcessor[] processDataRecords);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="rowPreFetch">The row pre-fetch size.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The process data record.</param>
        /// <returns>
        /// The number of fetched records.
        /// </returns>
        Task<int> ExecuteReader(int rowPreFetch, CancellationToken cancellationToken, params Action<IDataRecord>[] processDataRecord);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="rowPreFetch">The row pre fetch.</param>
        /// <param name="minFetchSize">Size of the min fetch.</param>
        /// <param name="maxFetchSize">Size of the max fetch.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The process data record.</param>
        /// <returns>
        /// The number of fetched records.
        /// </returns>
        Task<int> ExecuteReader(int rowPreFetch, int minFetchSize, int maxFetchSize, CancellationToken cancellationToken, params Action<IDataRecord>[] processDataRecord);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The data reader.</returns>
        Task<DbDataReader> ExecuteReader(CancellationToken cancellationToken);

        /// <summary>
        /// Executes the command returning a scalar.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// The scalar object.
        /// </returns>
        Task<object> ExecuteScalar(CancellationToken cancellationToken);

        /// <summary>
        /// Executes the command non query.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// Number affected records.
        /// </returns>
        Task<int> ExecuteNonQuery(CancellationToken cancellationToken);

        /// <summary>
        /// Updates a clob value.
        /// </summary>
        /// <param name="clobItems">The clob items.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>A task that can be used to monitor the progress of the operation.</returns>
        Task ExecuteCLOBUpdate(string[] clobItems, CancellationToken cancellationToken);
    }
}
