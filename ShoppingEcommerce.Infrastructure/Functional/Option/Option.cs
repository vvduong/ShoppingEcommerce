namespace ShoppingEcommerce.Infrastructure.Functional.Option
{
    public abstract class Option<T>
    {
        /// <summary>
        /// </summary>
        /// <param name="content"></param>
        public static implicit operator Option<T>(T content)
        {
            return new Some<T>(content);
        }

        /// <summary>
        /// </summary>
        /// <param name="none"></param>
        public static implicit operator Option<T>(None none)
        {
            return new None<T>();
        }
    }
}