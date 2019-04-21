using System;

namespace ShoppingEcommerce.Core.Abstraction
{
    public interface IBuildingSpecification<out T>
    {
        T Build();
    }
}