using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShoppingEcommerce.Core.Specifications
{
    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var leftVisitor = new ParameterReplaceExpressionVisitor(leftExpression.Parameters.Single(), parameter);
            var rightVisitor = new ParameterReplaceExpressionVisitor(rightExpression.Parameters.Single(), parameter);

            var andExpression = Expression.AndAlso(leftVisitor.Visit(leftExpression.Body), rightVisitor.Visit(rightExpression.Body));

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}