using System;
using System.Linq;
using System.Linq.Expressions;
using ShoppingEcommerce.Infrastructure.Extensions;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public class Specification<TEntity> : QuerySpecification<TEntity> where TEntity : class
    {
        public Specification() : this(entity => true)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        public Specification(Expression<Func<TEntity, bool>> expression) : base(expression)
        {
        }

        public static IQuerySpecification<TEntity> New()
        {
            return new Specification<TEntity>();
        }

        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQuerySpecification<TEntity> New(Expression<Func<TEntity, bool>> expression)
        {
            return new Specification<TEntity>(expression);
        }

        /// <summary>
        /// </summary>
        /// <param name="specifications"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> All(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecificationBuilder<TEntity>) New()
                , (current, specification) => current.And(specification));
        }

        /// <summary>
        /// </summary>
        /// <param name="specifications"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> None(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecificationBuilder<TEntity>) New()
                    , (current, specification) => current
                        .And(specification))
                .Not();
        }

        /// <summary>
        /// </summary>
        /// <param name="specifications"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Any(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecificationBuilder<TEntity>) New()
                , (current, specification) => current.Or(specification));
        }
    }
}