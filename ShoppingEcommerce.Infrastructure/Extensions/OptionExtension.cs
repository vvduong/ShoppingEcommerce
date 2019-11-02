using System;
using ShoppingEcommerce.Functional.Option;

namespace ShoppingEcommerce.Extensions
{
    public static class OptionExtension
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="option"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map)
        {
            return option is Some<T> some ? (Option<TResult>) map(some) : None.Value;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="whenNone"></param>
        /// <returns></returns>
        public static T Reduce<T>(this Option<T> option, T whenNone)
        {
            return option is Some<T> some ? (T) some : whenNone;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="whenNone"></param>
        /// <returns></returns>
        public static T Reduce<T>(this Option<T> option, Func<T> whenNone)
        {
            return option is Some<T> some ? (T) some : whenNone();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="action"></param>
        public static void Do<T>(this Option<T> option, Action<T> action)
        {
            if (option is Some<T> some)
            {
                action(some);
            }
        }
    }
}