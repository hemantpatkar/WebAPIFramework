using Framework.DomainModels.Master;
using Microsoft.EntityFrameworkCore;

namespace Framework.Repositories
{
    public class SharedDbContext : DbContext, ISharedDbContext
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityDbContext" /> class.
        /// </summary>
        /// <param name="options">The options used by the DbContext.</param>
        public SharedDbContext(DbContextOptions<SharedDbContext> options)
            : base(options)
        {
        }

        public DbSet<CountryMaster> CountryMaster { get; set; }

    }
}
