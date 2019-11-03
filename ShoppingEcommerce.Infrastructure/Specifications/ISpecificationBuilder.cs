namespace ShoppingEcommerce.Infrastructure.Specifications
{
    public interface ISpecificationBuilder<TEntity> where TEntity : class
    {
        SpecificationBuilder<TEntity> Internal { get; }
    }
}