using System.Linq.Expressions;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public class RebindParameterExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _newParameter;
        private readonly ParameterExpression _oldParameter;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="oldParameter"></param>
        /// <param name="newParameter"></param>
        public RebindParameterExpressionVisitor(ParameterExpression oldParameter
            , ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            return parameterExpression == _oldParameter
                ? _newParameter
                : base.VisitParameter(parameterExpression);
        }
    }
}