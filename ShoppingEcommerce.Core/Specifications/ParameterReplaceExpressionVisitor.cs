using System.Linq.Expressions;

namespace ShoppingEcommerce.Core.Specifications
{
    internal sealed class ParameterReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _left;
        private readonly Expression _right;

        internal ParameterReplaceExpressionVisitor(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override Expression Visit(Expression node)
        {
            return node == _left ? _right : base.Visit(node);
        }
    }
}