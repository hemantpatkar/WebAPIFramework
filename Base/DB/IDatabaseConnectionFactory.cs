namespace Base.DB
{
    /// <summary>
    /// Interface for a database connection factory.
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Creates a database connection for the default database.
        /// </summary>
        /// <returns>The database connection.</returns>
        IDatabaseConnection Create();

        /// <summary>
        /// Creates a database connection with the specified database id.
        /// </summary>
        /// <param name="databaseId">The database id.</param>
        /// <returns>The database connection.</returns>
        IDatabaseConnection Create(string databaseId);
    }
}
