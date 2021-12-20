using Framework.DomainModels.Master;
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
        DbSet<CountryMaster> CountryMaster { get; }
    }
}
