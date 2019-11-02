namespace ShoppingEcommerce.Specifications
{
    public interface IDynamicQuerySpecification<TEntity, out TValue>
        : IQuerySpecification<TEntity> where TEntity : class
    {
        TValue Value { get; }
    }
}