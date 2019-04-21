using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingEcommerce.Core.Abstraction
{
    /// <summary>
    /// Many objects have no conceptual identity. 
    /// 
    /// These objects describe characteristics of a thing.
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (!(obj is ValueObject valueObject))
            {
                return false;
            }

            if (GetType() != valueObject.GetType())
            {
                throw new ArgumentException($"Invalid comparison of Value Objects of different types: {GetType()} and {valueObject.GetType()}");
            }

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, property) =>
                {
                    unchecked
                    {
                        return current * 23 + (property?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
    }
}