using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShoppingEcommerce.Infrastructure.Utilities;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public class OrderSpecification<TEntity, TResult> : IOrderSpecification<TEntity> where TEntity : class
    {
        private readonly Expression<Func<TEntity, TResult>> _expression;

        private readonly OrderDirection _orderDirection;

        private Func<TEntity, TResult> _func;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="sort"></param>
        public OrderSpecification(OrderDirection sort = OrderDirection.Ascending) : this(entity => default(TResult)
            , sort)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderDirection"></param>
        public OrderSpecification(Expression<Func<TEntity, TResult>> expression,
            OrderDirection orderDirection = OrderDirection.Ascending)
        {
            _expression = expression;
            _orderDirection = orderDirection;
        }

        public SpecificationBuilder<TEntity> Internal => new SpecificationBuilder<TEntity>(this);

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query)
        {
            return _orderDirection == OrderDirection.Descending
                ? query.OrderByDescending(AsExpression())
                : query.OrderBy(AsExpression());
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query)
        {
            return _orderDirection == OrderDirection.Descending
                ? query.ThenByDescending(AsExpression())
                : query.ThenBy(AsExpression());
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> sequence)
        {
            return _orderDirection == OrderDirection.Descending
                ? sequence.OrderByDescending(AsFunc())
                : sequence.OrderBy(AsFunc());
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> sequence)
        {
            return _orderDirection == OrderDirection.Descending
                ? sequence.ThenByDescending(AsFunc())
                : sequence.ThenBy(AsFunc());
        }

        public virtual Expression<Func<TEntity, TResult>> AsExpression()
        {
            return _expression;
        }

        public Func<TEntity, TResult> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }

        /// <summary>
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IOrderSpecification<TEntity> New(OrderDirection sort = OrderDirection.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(sort);
        }

        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IOrderSpecification<TEntity> New(Expression<Func<TEntity, TResult>> expression,
            OrderDirection sort = OrderDirection.Ascending)
        {
            return new OrderSpecification<TEntity, TResult>(expression, sort);
        }
    }
}