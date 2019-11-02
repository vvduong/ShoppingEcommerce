using System;
using System.Linq;
using ShoppingEcommerce.Functional.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ShoppingEcommerce.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToCamelCaseJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Option<T> When<T>(this T value, Func<T, bool> predicate)
        {
            return predicate(value) ? (Option<T>) value : None.Value;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Option<T> When<T>(this T value, bool condition)
        {
            return condition ? (Option<T>) value : None.Value;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Option<T> NoneIfNull<T>(this T value)
        {
            return value.When(value != null);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] sequence)
        {
            return sequence.Contains(value);
        }
    }
}