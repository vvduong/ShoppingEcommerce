using System;
using System.Linq.Expressions;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public static class PredicateBuilder
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ExpressionStarter<T> New<T>(Expression<Func<T, bool>> expression = null)
        {
            return new ExpressionStarter<T>(expression);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultExpression"></param>
        /// <returns></returns>
        public static ExpressionStarter<T> New<T>(bool defaultExpression)
        {
            return new ExpressionStarter<T>(defaultExpression);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression"></param>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression
            , Expression<Func<T, bool>> rightExpression)
        {
            var expression = new RebindParameterExpressionVisitor(rightExpression.Parameters[0]
                , leftExpression.Parameters[0]).Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExpression.Body, expression)
                , leftExpression.Parameters);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression"></param>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression
            , Expression<Func<T, bool>> rightExpression)
        {
            var expression = new RebindParameterExpressionVisitor(rightExpression.Parameters[0]
                , leftExpression.Parameters[0]).Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExpression.Body, expression)
                , leftExpression.Parameters);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftExpression"></param>
        /// <param name="rightExpression"></param>
        /// <param name="predicateOperator"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Extend<T>(this Expression<Func<T, bool>> leftExpression
            , Expression<Func<T, bool>> rightExpression
            , PredicateOperator predicateOperator = PredicateOperator.Or)
        {
            return predicateOperator == PredicateOperator.Or
                ? leftExpression.Or(rightExpression)
                : leftExpression.And(rightExpression);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionStarter"></param>
        /// <param name="rightExpression"></param>
        /// <param name="predicateOperator"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Extend<T>(this ExpressionStarter<T> expressionStarter
            , Expression<Func<T, bool>> rightExpression
            , PredicateOperator predicateOperator = PredicateOperator.Or)
        {
            return predicateOperator == PredicateOperator.Or
                ? expressionStarter.Or(rightExpression)
                : expressionStarter.And(rightExpression);
        }
    }
}