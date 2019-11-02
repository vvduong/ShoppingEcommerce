using System;
using ShoppingEcommerce.Utilities;

namespace ShoppingEcommerce.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToFormatString(this DateTime dateTime)
        {
            return dateTime.ToString(ApplicationConvention.DateTimeFormat);
        }

        /// <summary>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="fomrat"></param>
        /// <returns></returns>
        public static string ToFormatString(this DateTime dateTime
            , string fomrat)
        {
            return dateTime.ToString(fomrat);
        }
    }
}