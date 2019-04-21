namespace ShoppingEcommerce.Mapper
{
    public interface IMapping
    {
    }

    public interface IMapping<T> : IMapping where T : class
    {
    }
}
