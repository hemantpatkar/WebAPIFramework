using Framework.DomainModels.Logging;
using Microsoft.EntityFrameworkCore;

namespace Framework.Repositories
{
    public interface ISharedDbContext : IDbContext
    {
        /// <summary>
        /// Gets the security Users.
        /// </summary>
        /// <value>
        /// The security Users.
        /// </value>
        DbSet<LogTable> LogTable { get; }
    }
}
