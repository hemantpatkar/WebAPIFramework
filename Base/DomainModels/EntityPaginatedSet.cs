using System.Collections.Generic;

namespace Base.DomainModels
{
    /// <summary>
    /// A paginated set of entities.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class EntityPaginatedSet<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPaginatedSet{T}"/> class.
        /// </summary>
        public EntityPaginatedSet()
        {
        }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<T> Entities
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a count.
        /// </summary>
        /// <value>
        /// A count.
        /// </value>
        public int Count
        {
            get;
            set;
        }
    }
}
