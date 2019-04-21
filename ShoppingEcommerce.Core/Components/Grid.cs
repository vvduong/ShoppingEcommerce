using System.Collections.Generic;
using ShoppingEcommerce.Mapper;

namespace ShoppingEcommerce.Core.Components
{
    public class Grid<T> where T : IMapping
    {
        public IReadOnlyList<T> Result { get; set; }

        public int Count { get; set; }
    }
}