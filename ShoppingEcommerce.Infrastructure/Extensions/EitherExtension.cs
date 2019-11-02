using System;
using ShoppingEcommerce.Functional.Either;

namespace ShoppingEcommerce.Extensions
{
    public static class EitherExtension
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <typeparam name="TMapRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Either<TLeft, TMapRight> Map<TLeft, TRight, TMapRight>(this Either<TLeft, TRight> either
            , Func<TRight, TMapRight> map)
        {
            return either is Right<TLeft, TRight> right
                ? (Either<TLeft, TMapRight>) map(right)
                : (TLeft) (Left<TLeft, TRight>) either;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <typeparam name="TMapRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Either<TLeft, TMapRight> Map<TLeft, TRight, TMapRight>(this Either<TLeft, TRight> either
            , Func<TRight, Either<TLeft, TMapRight>> map)
        {
            return either is Right<TLeft, TRight> right
                ? map(right)
                : (TLeft) (Left<TLeft, TRight>) either;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static TRight Reduce<TLeft, TRight>(this Either<TLeft, TRight> either
            , Func<TLeft, TRight> map)
        {
            return either is Left<TLeft, TRight> left
                ? map(left)
                : (Right<TLeft, TRight>) either;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TLeft"></typeparam>
        /// <typeparam name="TRight"></typeparam>
        /// <param name="either"></param>
        /// <param name="map"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(this Either<TLeft, TRight> either
            , Func<TLeft, TRight> map
            , Func<TLeft, bool> when)
        {
            return either is Left<TLeft, TRight> bound && when(bound)
                ? (Either<TLeft, TRight>) map(bound)
                : either;
        }
    }
}