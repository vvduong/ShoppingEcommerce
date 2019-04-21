using System;

namespace ShoppingEcommerce.Core.Functional
{
    public abstract class Option<T>
    {
        public abstract Option<TResult> Map<TResult>(Func<T, TResult> map);

        public abstract Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map);

        public abstract T Reduce(T whenNone);

        public abstract T Reduce(Func<T> whenNone);

        public Option<TResult> OfType<TResult>() where TResult : class
        {
            return this is Some<T> some && typeof(TResult).IsAssignableFrom(typeof(T))
                ? (Option<TResult>) new Some<TResult>(some.Value as TResult)
                : None.Value;
        }

        public static implicit operator Option<T>(T value) => new Some<T>(value);

        public static implicit operator Option<T>(None none) => new None<T>();
    }
}