using Framework.DomainModels.Master;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<StateMaster> stateMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            this.ConfigurePrimaryKeysSharedDbContext(modelBuilder);
            this.ConfigureForeignKeysSharedDbContext(modelBuilder);
           
        }

        private void ConfigurePrimaryKeysSharedDbContext(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CountryMaster>()
            //     .HasKey(entity => entity.ID);
            //modelBuilder.Entity<StateMaster>()
            //    .HasKey(entity => entity.ID);
        }
        private void ConfigureForeignKeysSharedDbContext(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StateMaster>()
            //      .HasOne(entity => entity.CountryMaster).WithMany().HasForeignKey(k => k.CountryID);
        }
 
    }
}
