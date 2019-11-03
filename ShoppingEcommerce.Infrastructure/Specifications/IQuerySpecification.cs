using System;
using System.Linq.Expressions;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public interface IQuerySpecification<TEntity> : ISpecificationBuilder<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> AsExpression();

        Func<TEntity, bool> AsFunc();
    }
}