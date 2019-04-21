using System;
using System.Collections.Generic;

namespace ShoppingEcommerce.Core.Functional
{
    public sealed class Some<T> : Option<T>, IEquatable<Some<T>>
    {
        private string ValueToString => Value?.ToString() ?? "<null>";

        public T Value { get; }

        public Some(T value)
        {
            Value = value;
        }

        public override Option<TResult> Map<TResult>(Func<T, TResult> map) => map(Value);

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) => map(Value);

        public override T Reduce(T whenNone) => Value;

        public override T Reduce(Func<T> whenNone) => Value;

        public bool Equals(Some<T> some)
        {
            if (some is null)
            {
                return false;
            }

            if (ReferenceEquals(this, some))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(Value, some.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Some<T> && Equals((Some<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public override string ToString() => $"Some({ValueToString})";

        public static implicit operator T(Some<T> some) => some.Value;

        public static implicit operator Some<T>(T value) => new Some<T>(value);

        public static bool operator ==(Some<T> left, Some<T> right)
        {
            return (left is null && right is null) || (!(left is null) && left.Equals(right));
        }

        public static bool operator !=(Some<T> left, Some<T> right)
        {
            return !(left == right);
        }
    }
}