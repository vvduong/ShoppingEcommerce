using System.Collections.Generic;
using System.Linq;

namespace ShoppingEcommerce.Specifications
{
    public interface IOrderSpecification<TEntity> : ISpecificationBuilder<TEntity> where TEntity : class
    {
        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query);

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query);

        /// <summary>
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> sequence);

        /// <summary>
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> sequence);
    }
}