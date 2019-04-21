using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShoppingEcommerce.Core.Specifications
{
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var parameter = Expression.Parameter(typeof(T));

            var expression = _specification.ToExpression();

            var visitor = new ParameterReplaceExpressionVisitor(expression.Parameters.Single(), parameter);

            var notExpression = Expression.Not(visitor.Visit(expression.Body));

            return Expression.Lambda<Func<T, bool>>(notExpression, parameter);
        }
    }
}