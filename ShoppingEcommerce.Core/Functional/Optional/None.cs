using System;

namespace ShoppingEcommerce.Core.Functional
{
    public sealed class None : IEquatable<None>
    {
        public static None Value { get; } = new None();

        private None() {

        }

        public override bool Equals(object obj)
        {
            return !(obj is null) && ((obj is None) || IsGenericNone(obj.GetType()));
        }

        public bool Equals(None none) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "None";

        private bool IsGenericNone(Type type)
        {
            return type.GenericTypeArguments.Length == 1
                && typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;
        }
    }

    public sealed class None<T> : Option<T>, IEquatable<None<T>>, IEquatable<None>
    {
        public override Option<TResult> Map<TResult>(Func<T, TResult> map) => None.Value;

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) => None.Value;

        public override T Reduce(T whenNone) => whenNone;

        public override T Reduce(Func<T> whenNone) => whenNone();

        public bool Equals(None none) => true;

        public bool Equals(None<T> none) => true;

        public override bool Equals(object obj)
        {
            return !(obj is null) && ((obj is None) || (obj is None<T>));
        }

        public override int GetHashCode() => 0;

        public override string ToString() => "None";

        public static bool operator ==(None<T> left, None<T> right)
        {
            return (left is null && right is null) || (!(left is null) && left.Equals(right));
        }

        public static bool operator !=(None<T> left, None<T> right)
        {
            return !(left == right);
        }
    }
}