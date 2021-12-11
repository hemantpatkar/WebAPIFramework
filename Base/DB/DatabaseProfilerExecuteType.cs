namespace Base.DB
{
    /// <summary>
    /// Type of database execution.
    /// </summary>
    public enum DatabaseProfilerExecuteType
    {
        /// <summary>
        /// Unknown execute type.
        /// </summary>
        None = 0,

        /// <summary>
        /// DML statements that alter database state, e.g. INSERT, UPDATE.
        /// </summary>
        NonQuery = 1,

        /// <summary>
        /// Statements that return a single record.
        /// </summary>
        Scalar = 2,

        /// <summary>
        /// Statements that iterate over a result set.
        /// </summary>
        Reader = 3,
    }
}
