using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    /// <summary>
    /// Base methods and properties for all Entity Framework database contexts.
    /// </summary>
    public interface IDbContext
    {

        DatabaseFacade Database { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
            where TEntity : class;


        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
            where TEntity : class;


        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class;


        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
    }
}
