﻿using System;

namespace ShoppingEcommerce.Core.Functional.Optional.Extensions
{
    public static class ObjectExtension
    {
        public static Option<T> When<T>(this T obj, bool condition)
        {
            return condition 
                ? (Option<T>) new Some<T>(obj) 
                : None.Value;
        }

        public static Option<T> When<T>(this T obj, Func<T, bool> predicate)
        {
            return obj.When(predicate(obj));
        }

        public static Option<T> NoneIfNull<T>(this T obj) where T : class
        {
            return obj.When(!(obj is null));
        }
    }
}