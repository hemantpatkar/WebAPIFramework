using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Base.DB.SQLServer
{
    /// <summary>
    /// Class responsible to execute query in the database. This class is wrapper for SQLCommand of the ADO.NET native code. Implements <see cref="IDatabaseCommand" />.
    /// </summary>
    public class SqlServerDatabaseCommand : IDatabaseCommand
    {
        /// <summary>
        /// The database profiler.
        /// </summary>
        private readonly IDatabaseProfiler databaseProfiler;

        /// <summary>
        /// The SqlServer server command.
        /// </summary>
        private SqlCommand command;

        /// <summary>Initializes a new instance of the <see cref="SqlServerDatabaseCommand"/> class.</summary>
        /// <param name="databaseProfiler">Interface for a database profiler.</param>
        public SqlServerDatabaseCommand(IDatabaseProfiler databaseProfiler)
        {
            this.databaseProfiler = databaseProfiler;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlServerDatabaseCommand" /> class.
        /// </summary>
        ~SqlServerDatabaseCommand()
        {
            this.Dispose(false);
        }

        #region IDBCommand Members

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        public CommandType CommandType
        {
            get
            {
                return this.command.CommandType;
            }

            set
            {
                this.command.CommandType = value;
            }
        }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        public string CommandText
        {
            get
            {
                return this.command.CommandText;
            }

            set
            {
                this.command.CommandText = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public int CommandTimeout
        {
            get
            {
                return this.command.CommandTimeout;
            }

            set
            {
                this.command.CommandTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial size of the LOB fetch.
        /// </summary>
        /// <value>
        /// The initial size of the LOB fetch.
        /// </value>
        public int InitialLOBFetchSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fetch size.
        /// </summary>
        /// <value>
        /// The fetch size.
        /// </value>
        public long FetchSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the initial LOB fetch size config.
        /// </summary>
        /// <value>
        /// The initial LOB fetch size config.
        /// </value>
        public int? InitialLOBFetchSizeConfig
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the command's parameter collection.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public DbParameterCollection Parameters
        {
            get
            {
                return this.command.Parameters;
            }
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Setups the specified SqlServer command.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the sqlCommand argument is null.</exception>
        public void Setup(SqlCommand sqlCommand)
        {
            this.command = sqlCommand ?? throw new ArgumentNullException(nameof(sqlCommand));
        }

        /// <summary>
        /// Create a database parameter with direction Out.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// A database parameter with direction Out.
        /// </returns>
        public DbParameter AddParameterOut<T>(string parameterName)
        {
            return this.AddParameterOut<T>(parameterName, 0, 0);
        }

        /// <summary>
        /// Adds the parameter in out.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter with direction In Out filled by default with a value.
        /// </returns>
        public DbParameter AddParameterInOut<T>(string parameterName, T value)
        {
            DbParameter parameter = this.AddParameter<T>(parameterName);
            parameter.Direction = ParameterDirection.InputOutput;
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }

            return parameter;
        }

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
        public DbParameter AddParameterOut<T>(string parameterName, byte precision, byte scale)
        {
            DbParameter parameter = this.AddParameter<T>(parameterName);
            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        /// <summary>
        /// Create a database parameter.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// A database parameter.
        /// </returns>
        public DbParameter AddParameter<T>(string parameterName)
        {
            InferDatabaseTypeResult inferedDatabaseType = this.InferDatabaseType(typeof(T));
            SqlParameter parameter = this.command.Parameters.Add(parameterName, inferedDatabaseType.DbType);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DBNull.Value;
            return parameter;
        }

        /// <summary>
        /// Create a database parameter.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter.
        /// </returns>
        public DbParameter AddParameter<T>(string parameterName, T value)
        {
            DbParameter parameter = this.AddParameter<T>(parameterName);
            if (value != null)
            {
                parameter.Value = value;
                if (typeof(T).IsArray)
                {
                    parameter.Size = (value as Array).Length;
                }
            }

            return parameter;
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter.
        /// </returns>
        public DbParameter AddParameter<T>(string parameterName, object value)
        {
            DbParameter parameter = this.AddParameter<T>(parameterName);
            if (value != null)
            {
                parameter.Value = value;
                if (typeof(T).IsArray)
                {
                    parameter.Size = (value as Array).Length;
                }
            }

            return parameter;
        }

        /// <summary>
        /// Create a database parameter.
        /// </summary>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the type argument is null.</exception>
        public DbParameter AddParameter(Type type, string parameterName, object value)
        {
            return this.AddParameter(type, parameterName, value, null);
        }

        /// <summary>
        /// Create a database parameter.
        /// </summary>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="databaseTypeName">Name of the database type.</param>
        /// <returns>
        /// A database parameter.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the type argument is null.</exception>
        public DbParameter AddParameter(Type type, string parameterName, object value, string databaseTypeName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            InferDatabaseTypeResult inferedDatabaseType = this.InferDatabaseType(type);

            SqlParameter parameter = this.command.Parameters.Add(parameterName, inferedDatabaseType.DbType);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DBNull.Value;
            parameter.TypeName = databaseTypeName;
            if (value != null)
            {
                parameter.Value = value;
                if (type.IsArray)
                {
                    parameter.Size = (value as Array).Length;
                }
            }

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type CLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter of type CLOB.
        /// </returns>
        public DbParameter AddParameterCLOB(string parameterName, string value)
        {
            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = string.IsNullOrEmpty(value) ? (object)DBNull.Value : value;

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type CLOB with direction Out.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// A database parameter of type CLOB with direction Out.
        /// </returns>
        public DbParameter AddParameterOutCLOB(string parameterName)
        {
            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type NCLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter of type NCLOB.
        /// </returns>
        public DbParameter AddParameterNCLOB(string parameterName, string value)
        {
            /*return command.Parameters.Add(
                FixParameterName(parameterName), SqlDbType.NClob,
                (string.IsNullOrEmpty(value) ? (object)DBNull.Value : value),
                ParameterDirection.Input
            );*/

            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = string.IsNullOrEmpty(value) ? (object)DBNull.Value : value;

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type NCLOB with direction Out.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// A database parameter of type NCLOB with direction Out.
        /// </returns>
        public DbParameter AddParameterOutNCLOB(string parameterName)
        {
            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The stream.</param>
        /// <returns>
        /// A database parameter of type BLOB.
        /// </returns>
        public DbParameter AddParameterBLOB(string parameterName, Stream value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            byte[] byteData = new byte[(int)value.Length];
            value.Read(byteData, 0, (int)value.Length);

            return this.AddParameterBLOB(parameterName, byteData);
        }

        /// <summary>
        /// Create a database parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A database parameter of type BLOB.
        /// </returns>
        public DbParameter AddParameterBLOB(string parameterName, byte[] value)
        {
            /*return command.Parameters.Add(
                FixParameterName(parameterName), SqlDbType.Blob,
                value, ParameterDirection.Input);*/

            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.VarBinary);
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create a database parameter of type BLOB.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>
        /// A database parameter of type BLOB.
        /// </returns>
        public DbParameter AddParameterBLOB(string parameterName)
        {
            SqlParameter parameter = this.command.Parameters.Add(parameterName, SqlDbType.VarBinary, -1);
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        /// <summary>
        /// Adds the parameter of type RefCursor.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>The database parameter.</returns>
        public DbParameter AddParameterRefCursor(string parameterName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="databaseType">Type of the database.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The database parameter.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Thrown when the specified database type is not implemented.</exception>
        public DbParameter AddParameter(DbType databaseType, string parameterName, object value)
        {
            switch (databaseType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return this.AddParameter<string>(parameterName, value);
                case DbType.Binary:
                    return this.AddParameter<byte[]>(parameterName, value);
                case DbType.Boolean:
                    return this.AddParameter<bool>(parameterName, value);
                case DbType.Date:
                case DbType.DateTime2:
                case DbType.DateTime:
                    return this.AddParameter<DateTime>(parameterName, value);
                case DbType.DateTimeOffset:
                    return this.AddParameter<DateTimeOffset>(parameterName, value);
                case DbType.Decimal:
                    return this.AddParameter<decimal>(parameterName, value);
                case DbType.Double:
                    return this.AddParameter<double>(parameterName, value);
                case DbType.Guid:
                    return this.AddParameter<Guid>(parameterName, value);
                case DbType.Int16:
                    return this.AddParameter<short>(parameterName, value);
                case DbType.Int32:
                    return this.AddParameter<int>(parameterName, value);
                case DbType.Int64:
                    return this.AddParameter<long>(parameterName, value);
                case DbType.UInt16:
                    return this.AddParameter<ushort>(parameterName, value);
                case DbType.UInt32:
                    return this.AddParameter<uint>(parameterName, value);
                case DbType.UInt64:
                    return this.AddParameter<ulong>(parameterName, value);
                case DbType.Currency:
                case DbType.Single:
                    return this.AddParameter<float>(parameterName, value);
                case DbType.SByte:
                case DbType.Byte:
                    return this.AddParameter<byte>(parameterName, value);
                case DbType.VarNumeric:
                case DbType.Xml:
                case DbType.Time:
                case DbType.Object:

                default:
                    throw new NotSupportedException("Cannot infer native database type for type : " + databaseType);
            }
        }

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
        public DbParameter AddParameter(string typeFullName, string parameterName, object value, string databaseTypeName)
        {
            Type type = Type.GetType(typeFullName);
            return this.AddParameter(type, parameterName, value, databaseTypeName);
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The data record processor.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        public async Task<int> ExecuteReader(CancellationToken cancellationToken, params Action<System.Data.IDataRecord>[] processDataRecord)
        {
            return await this.ExecuteReader(0, 65536, 1048576, cancellationToken, processDataRecord).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query with a number of rows pre fetched during each round-trip to the database.
        /// </summary>
        /// <param name="rowPreFetch">Number of rows to pre fetch.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The process data record.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        public async Task<int> ExecuteReader(int rowPreFetch, CancellationToken cancellationToken, params Action<System.Data.IDataRecord>[] processDataRecord)
        {
            return await this.ExecuteReader(rowPreFetch, 65536, 1048576, cancellationToken, processDataRecord).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query with a number of rows pre fetched, min and a max amount of data fetched during each round-trip to the database.
        /// </summary>
        /// <param name="rowPreFetch">Number of rows to pre fetch.</param>
        /// <param name="minFetchSize">Minimum fetch size (bytes).</param>
        /// <param name="maxFetchSize">Maximum fetch size (bytes).</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecord">The process data record.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when processDataRecord argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Throw when the processDataRecord argument is null or has a length equal to zero.</exception>
        public virtual async Task<int> ExecuteReader(int rowPreFetch, int minFetchSize, int maxFetchSize, CancellationToken cancellationToken, params Action<System.Data.IDataRecord>[] processDataRecord)
        {
            if (processDataRecord == null)
            {
                throw new ArgumentNullException(nameof(processDataRecord));
            }

            if (processDataRecord.Length == 0)
            {
                throw new ArgumentException("The processor cannot be empty", nameof(processDataRecord));
            }

            int result = 0;

            using (DbDataReader reader = await this.ExecuteReader(cancellationToken).ConfigureAwait(false))
            {
                bool hasNextResult = true;
                int indexProcessDataRecord = 0;

                while (hasNextResult && indexProcessDataRecord < processDataRecord.Length)
                {
                    Action<System.Data.IDataRecord> currentProcessDataRecord = processDataRecord[indexProcessDataRecord];

                    ////if (rowPreFetch != 0 && reader.RowSize != 0) reader.FetchSize = Math.Max(Math.Min(reader.RowSize * rowPreFetch, maxFetchSize), minFetchSize);

                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        currentProcessDataRecord(reader);

                        if (indexProcessDataRecord == 0)
                        {
                            result++;
                        }
                    }

                    hasNextResult = await reader.NextResultAsync(cancellationToken).ConfigureAwait(false);
                    indexProcessDataRecord++;
                }

                this.ReaderFinishInternal(reader);
            }

            return result;
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <param name="processDataRecords">The data record processor.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        public async Task<int> ExecuteReader(CancellationToken cancellationToken, params DataRecordProcessor[] processDataRecords)
        {
            if (processDataRecords == null)
            {
                throw new ArgumentNullException(nameof(processDataRecords));
            }

            if (processDataRecords.Length == 0)
            {
                throw new ArgumentException("The processors cannot be empty", nameof(processDataRecords));
            }

            int result = 0;

            using (DbDataReader reader = await this.ExecuteReader(cancellationToken).ConfigureAwait(false))
            {
                bool hasNextResult = true;
                int indexProcessDataRecord = 0;

                while (hasNextResult && indexProcessDataRecord < processDataRecords.Length)
                {
                    var currentProcessDataRecord = processDataRecords[indexProcessDataRecord];

                    if (currentProcessDataRecord.PreProcessRecords != null)
                    {
                        await currentProcessDataRecord.PreProcessRecords(reader, cancellationToken).ConfigureAwait(false);
                    }

                    ////if (rowPreFetch != 0 && reader.RowSize != 0) reader.FetchSize = Math.Max(Math.Min(reader.RowSize * rowPreFetch, maxFetchSize), minFetchSize);

                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        if (currentProcessDataRecord.ProcessRecord != null)
                        {
                            await currentProcessDataRecord.ProcessRecord(reader, cancellationToken).ConfigureAwait(false);
                        }

                        if (indexProcessDataRecord == 0)
                        {
                            result++;
                        }
                    }

                    hasNextResult = await reader.NextResultAsync(cancellationToken).ConfigureAwait(false);
                    indexProcessDataRecord++;

                    if (currentProcessDataRecord.PostProcessRecords != null)
                    {
                        await currentProcessDataRecord.PostProcessRecords(cancellationToken).ConfigureAwait(false);
                    }
                }

                this.ReaderFinishInternal(reader);
            }

            return result;
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query with a number of rows pre fetched, min and a max amount of data fetched during each round-trip to the database.
        /// </summary>
        /// <param name="processDataRecord">The data record processor.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        public async Task<bool> ExecuteReaderSingle(Action<System.Data.IDataRecord> processDataRecord, CancellationToken cancellationToken)
        {
            return await this.ExecuteReaderSingle(65536, 1048576, processDataRecord, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the query, and returns the result set returned by the query with a number of rows pre fetched, min and a max amount of data fetched during each round-trip to the database.
        /// </summary>
        /// <param name="minFetchSize">Minimum fetch size (bytes).</param>
        /// <param name="maxFetchSize">Maximum fetch size (bytes).</param>
        /// <param name="processDataRecord">The process data record.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Database data reader with the result set.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throw when the processDataRecord argument is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the processDataRecord argument is null.</exception>
        public virtual async Task<bool> ExecuteReaderSingle(int minFetchSize, int maxFetchSize, Action<System.Data.IDataRecord> processDataRecord, CancellationToken cancellationToken)
        {
            if (processDataRecord == null)
            {
                throw new ArgumentNullException(nameof(processDataRecord));
            }

            bool result = false;

            using (DbDataReader reader = await this.ExecuteReader(cancellationToken).ConfigureAwait(false))
            {
                ////if (reader.RowSize != 0) reader.FetchSize = Math.Max(Math.Min(reader.RowSize * 1, maxFetchSize), minFetchSize);
                if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    processDataRecord(reader);
                    result = true;
                }

                this.ReaderFinishInternal(reader);
            }

            return result;
        }

        /// <summary>
        /// Executes the reader internal.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The sql reader.
        /// </returns>
        public virtual async Task<DbDataReader> ExecuteReader(CancellationToken cancellationToken)
        {
            DbDataReader result = null;

            if (this.databaseProfiler == null || !this.databaseProfiler.IsActive)
            {
                result = await this.command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                this.databaseProfiler.ExecuteStart(this.command, DatabaseProfilerExecuteType.Reader);
                try
                {
                    result = await this.command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.databaseProfiler.OnError(this.command, DatabaseProfilerExecuteType.Reader, e);
                    throw;
                }
                finally
                {
                    this.databaseProfiler.ExecuteFinish(this.command, DatabaseProfilerExecuteType.Reader, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference.
        /// </returns>
        public virtual async Task<object> ExecuteScalar(CancellationToken cancellationToken)
        {
            object result = null;

            if (this.databaseProfiler == null || !this.databaseProfiler.IsActive)
            {
                result = await this.command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                this.databaseProfiler.ExecuteStart(this.command, DatabaseProfilerExecuteType.Scalar);
                try
                {
                    result = await this.command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.databaseProfiler.OnError(this.command, DatabaseProfilerExecuteType.Scalar, e);
                    throw;
                }
                finally
                {
                    this.databaseProfiler.ExecuteFinish(this.command, DatabaseProfilerExecuteType.Scalar, null);
                }
            }

            return result;
        }

        /// <summary>
        /// Executes a PL/SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>
        /// Integer with the affected records.
        /// </returns>
        public virtual async Task<int> ExecuteNonQuery(CancellationToken cancellationToken)
        {
            int result = 0;

            if (this.databaseProfiler == null || !this.databaseProfiler.IsActive)
            {
                result = await this.command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                this.databaseProfiler.ExecuteStart(this.command, DatabaseProfilerExecuteType.NonQuery);
                try
                {
                    result = await this.command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.databaseProfiler.OnError(this.command, DatabaseProfilerExecuteType.NonQuery, e);
                    throw;
                }
                finally
                {
                    this.databaseProfiler.ExecuteFinish(this.command, DatabaseProfilerExecuteType.NonQuery, null);
                }
            }

            return result;
        }

        /// <summary>
        /// Updates a clob value.
        /// </summary>
        /// <param name="clobItems">The clob items.</param>
        /// <param name="cancellationToken">The cancelation token.</param>
        /// <returns>A <see cref="Task"/> that can be used to monitor the progress of the operation.</returns>
        /// <exception cref="NotSupportedException">This method is not supported for SqlServer.</exception>
        public virtual Task ExecuteCLOBUpdate(string[] clobItems, CancellationToken cancellationToken)
        {
            return Task.FromException(new NotSupportedException());
        }

        /// <summary>
        /// Infers the type of the database parameter.
        /// </summary>
        /// <param name="type">Value type.</param>
        /// <returns>
        /// An InferDatabaseTypeResult result structure.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Thrown when the conversion is not supported.</exception>
        protected InferDatabaseTypeResult InferDatabaseType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            InferDatabaseTypeResult result;

            Type innerType = type;

            // Validates if the innerType is nullable and if so returns the underling type
            if (innerType.IsGenericType && innerType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                innerType = Nullable.GetUnderlyingType(innerType);
            }

            if (innerType.IsEnum)
            {
                innerType = Enum.GetUnderlyingType(innerType);
            }

            if (innerType == typeof(int))
            {
                result.DbType = SqlDbType.Int;
                return result;
            }

            if (innerType == typeof(byte[]))
            {
                result.DbType = SqlDbType.VarBinary;
                return result;
            }

            if (innerType == typeof(decimal))
            {
                result.DbType = SqlDbType.Money;
                return result;
            }

            if (innerType == typeof(string))
            {
                result.DbType = SqlDbType.NVarChar;
                return result;
            }

            if (innerType == typeof(DateTime))
            {
                result.DbType = SqlDbType.DateTime2;
                return result;
            }

            if (innerType == typeof(DateTimeOffset))
            {
                result.DbType = SqlDbType.DateTimeOffset;
                return result;
            }

            if (innerType == typeof(bool))
            {
                result.DbType = SqlDbType.TinyInt;
                return result;
            }

            if (innerType == typeof(Guid))
            {
                result.DbType = SqlDbType.UniqueIdentifier;
                return result;
            }

            if (innerType == typeof(Enum))
            {
                result.DbType = SqlDbType.TinyInt;
                return result;
            }

            if (innerType == typeof(short))
            {
                result.DbType = SqlDbType.SmallInt;
                return result;
            }

            if (innerType == typeof(long))
            {
                result.DbType = SqlDbType.BigInt;
                return result;
            }

            if (innerType == typeof(double))
            {
                result.DbType = SqlDbType.Float;
                return result;
            }

            if (innerType == typeof(float))
            {
                result.DbType = SqlDbType.Real;
                return result;
            }

            if (innerType == typeof(DataTable))
            {
                result.DbType = SqlDbType.Structured;
                return result;
            }

            if (innerType == typeof(byte))
            {
                result.DbType = SqlDbType.TinyInt;
                return result;
            }

            throw new NotSupportedException("Cannot infer native database type for type : " + innerType.FullName);
        }

        /// <summary>
        /// Finishes the reader internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual void ReaderFinishInternal(DbDataReader reader)
        {
            if (this.databaseProfiler == null || !this.databaseProfiler.IsActive)
            {
            }
            else
            {
                this.databaseProfiler.ReaderFinish(reader);
            }
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
                if (this.command != null)
                {
                    this.command.Dispose();
                }
            }
            //// Clean up all native resources
        }

        /// <summary>
        /// The Infer Database Type result structure.
        /// </summary>
        protected struct InferDatabaseTypeResult : IEquatable<InferDatabaseTypeResult>
        {
            /// <summary>
            /// SQL Database type.
            /// </summary>
            internal SqlDbType DbType;

            /// <summary>
            /// Implements the operator ==.
            /// </summary>
            /// <param name="inferDatabaseTypeResult1">The infer database type result1.</param>
            /// <param name="inferDatabaseTypeResult2">The infer database type result2.</param>
            /// <returns>
            /// The result of the operator.
            /// </returns>
            public static bool operator ==(InferDatabaseTypeResult inferDatabaseTypeResult1, InferDatabaseTypeResult inferDatabaseTypeResult2)
            {
                return inferDatabaseTypeResult1.Equals(inferDatabaseTypeResult2);
            }

            /// <summary>
            /// Implements the operator !=.
            /// </summary>
            /// <param name="inferDatabaseTypeResult1">The infer database type result1.</param>
            /// <param name="inferDatabaseTypeResult2">The infer database type result2.</param>
            /// <returns>
            /// The result of the operator.
            /// </returns>
            public static bool operator !=(InferDatabaseTypeResult inferDatabaseTypeResult1, InferDatabaseTypeResult inferDatabaseTypeResult2)
            {
                return !inferDatabaseTypeResult1.Equals(inferDatabaseTypeResult2);
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
            /// </returns>
            public override int GetHashCode()
            {
                return this.DbType.GetHashCode();
            }

            /// <summary>
            /// Determines whether the specified <see cref="object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
            /// <returns>
            ///   <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
            /// </returns>
            public override bool Equals(object obj)
            {
                if (!(obj is InferDatabaseTypeResult))
                {
                    return false;
                }

                return this.Equals((InferDatabaseTypeResult)obj);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
            /// </returns>
            public bool Equals(InferDatabaseTypeResult other)
            {
                if (this.DbType != other.DbType)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
