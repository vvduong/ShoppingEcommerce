namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public interface IOrderedSpecification<TEntity> : ISpecificationBuilder<TEntity> where TEntity : class
    {
    }
}