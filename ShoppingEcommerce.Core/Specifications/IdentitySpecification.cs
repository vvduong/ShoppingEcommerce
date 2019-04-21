using System;
using System.Linq.Expressions;

namespace ShoppingEcommerce.Core.Specifications
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return t => true;
        }
    }
}