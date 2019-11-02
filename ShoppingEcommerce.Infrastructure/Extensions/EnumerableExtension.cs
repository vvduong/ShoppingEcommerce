using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingEcommerce.Functional.Option;
using ShoppingEcommerce.Specifications;

namespace ShoppingEcommerce.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Flatten<T, TResult>(this IEnumerable<T> sequence
            , Func<T, Option<TResult>> map)
        {
            return sequence
                .Select(map)
                .OfType<Some<TResult>>()
                .Select(selector => (TResult) selector);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence)
        {
            return sequence
                .Select<T, Option<T>>(selector => selector)
                .DefaultIfEmpty(None.Value)
                .First();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence
            , Func<T, bool> predicate)
        {
            return sequence
                .Where(predicate)
                .FirstOrNone();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> sequence
            , Func<TSource, TKey> keySelector)
        {
            var keys = new HashSet<TKey>();

            foreach (var element in sequence)
            {
                if (keys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="specificationBuilder"></param>
        /// <param name="skipSort"></param>
        /// <returns></returns>
        public static IEnumerable<T> ExeSpec<T>(this IEnumerable<T> sequence
            , ISpecificationBuilder<T> specificationBuilder
            , bool skipSort = false) where T : class
        {
            var specificationBuilderInternal = specificationBuilder.Internal;

            var querySpecification = specificationBuilderInternal.QuerySpecification;

            var orderSpecifications = specificationBuilderInternal.OrderSpecifications;

            if (querySpecification != null)
            {
                sequence = sequence.Where(querySpecification.AsFunc());
            }

            if (skipSort)
            {
                return sequence;
            }

            var ordered = orderSpecifications.FirstOrDefault()?.InvokeSort(sequence);

            for (var i = 1; i < orderSpecifications.Count; i++)
            {
                ordered = orderSpecifications[i].InvokeSort(ordered);
            }

            return SkipTake(ordered ?? sequence
                , specificationBuilderInternal);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="specificationBuilder"></param>
        /// <returns></returns>
        private static IEnumerable<T> SkipTake<T>(IEnumerable<T> sequence
            , SpecificationBuilder<T> specificationBuilder) where T : class
        {
            if (specificationBuilder.Skip.HasValue)
            {
                sequence = sequence.Skip(specificationBuilder.Skip.Value);
            }

            if (specificationBuilder.Take.HasValue)
            {
                sequence = sequence.Take(specificationBuilder.Take.Value);
            }

            return sequence;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> sequence
            , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            return orderSpecification.InvokeSort(sequence);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TEntity> ThenBy<TEntity>(this IOrderedEnumerable<TEntity> sequence
            , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            return orderSpecification.InvokeSort(sequence);
        }
    }
}