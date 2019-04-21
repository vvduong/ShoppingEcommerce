using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingEcommerce.Core.Functional.Optional.Extensions
{
    public static class SequenceExtension
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence)
        {
            return sequence
                .Select(selector => (Option<T>) new Some<T>(selector))
                .DefaultIfEmpty(None.Value)
                .First();
        }

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence
                .Where(predicate)
                .FirstOrNone();
        }

        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> sequence, TKey key)
        {
            return sequence.TryGetValue(key, out TValue value)
                ? (Option<TValue>) new Some<TValue>(value)
                : None.Value;
        }

        public static IEnumerable<TResult> SelectOptional<T, TResult>(this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence
                .Select(map)
                .OfType<Some<TResult>>()
                .Select(some => some.Value);
        }
    }
}