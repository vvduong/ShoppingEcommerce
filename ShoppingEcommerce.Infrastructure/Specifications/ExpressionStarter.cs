using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public class ExpressionStarter<T>
    {
        private Expression<Func<T, bool>> _predicate;

        internal ExpressionStarter() : this(false)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="defaultExpression"></param>
        internal ExpressionStarter(bool defaultExpression)
        {
            if (defaultExpression)
            {
                DefaultExpression = t => true;
            }
            else
            {
                DefaultExpression = t => false;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        internal ExpressionStarter(Expression<Func<T, bool>> expression) : this(false)
        {
            _predicate = expression;
        }

        private Expression<Func<T, bool>> Predicate => IsStarted || !UseDefaultExpression
            ? _predicate
            : DefaultExpression;

        public bool IsStarted => _predicate != null;

        public bool UseDefaultExpression => DefaultExpression != null;

        public Expression<Func<T, bool>> DefaultExpression { get; set; }

        public virtual bool CanReduce => Predicate.CanReduce;

        public Expression Body => Predicate.Body;

        public ExpressionType NodeType => Predicate.NodeType;

        public ReadOnlyCollection<ParameterExpression> Parameters => Predicate.Parameters;

        public Type Type => Predicate.Type;

        public string Name => Predicate.Name;

        public Type ReturnType => Predicate.ReturnType;

        public bool TailCall => Predicate.TailCall;

        /// <summary>
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> Start(Expression<Func<T, bool>> expression)
        {
            if (IsStarted)
            {
                throw new InvalidOperationException(@"Predicate cannot be started again.");
            }

            return _predicate = expression;
        }

        /// <summary>
        /// </summary>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> rightExpression)
        {
            return IsStarted
                ? _predicate = Predicate.Or(rightExpression)
                : Start(rightExpression);
        }

        /// <summary>
        /// </summary>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> And(Expression<Func<T, bool>> rightExpression)
        {
            return IsStarted
                ? _predicate = Predicate.And(rightExpression)
                : Start(rightExpression);
        }

        public override string ToString()
        {
            return Predicate?.ToString() ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// </summary>
        /// <param name="rightExpression"></param>
        public static implicit operator Expression<Func<T, bool>>(ExpressionStarter<T> rightExpression)
        {
            return rightExpression?.Predicate;
        }

        /// <summary>
        /// </summary>
        /// <param name="rightExpression"></param>
        public static implicit operator Func<T, bool>(ExpressionStarter<T> rightExpression)
        {
            return rightExpression != null
                ? rightExpression.IsStarted || rightExpression.UseDefaultExpression
                    ? rightExpression.Predicate.Compile()
                    : null
                : null;
        }

        /// <summary>
        /// </summary>
        /// <param name="righteExpression"></param>
        public static implicit operator ExpressionStarter<T>(Expression<Func<T, bool>> righteExpression)
        {
            return righteExpression == null ? null : new ExpressionStarter<T>(righteExpression);
        }

        public Func<T, bool> Compile()
        {
            return Predicate.Compile();
        }

        /// <summary>
        /// </summary>
        /// <param name="debugInfoGenerator"></param>
        /// <returns></returns>
        public Func<T, bool> Compile(DebugInfoGenerator debugInfoGenerator)
        {
            return Predicate.Compile(debugInfoGenerator);
        }

        /// <summary>
        /// </summary>
        /// <param name="body"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Expression<Func<T, bool>> Update(Expression body
            , IEnumerable<ParameterExpression> parameters)
        {
            return Predicate.Update(body, parameters);
        }

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        public void CompileToMethod(MethodBuilder method)
        {
            Predicate.CompileToMethod(method);
        }

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="debugInfoGenerator"></param>
        public void CompileToMethod(MethodBuilder method
            , DebugInfoGenerator debugInfoGenerator)
        {
            Predicate.CompileToMethod(method, debugInfoGenerator);
        }
    }
}