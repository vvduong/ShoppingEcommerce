namespace ShoppingEcommerce.Infrastructure.Functional.Option
{
    public class Some<T> : Option<T>
    {
        /// <summary>
        /// </summary>
        /// <param name="content"></param>
        public Some(T content)
        {
            Content = content;
        }

        private T Content { get; }

        /// <summary>
        /// </summary>
        /// <param name="some"></param>
        public static implicit operator T(Some<T> some)
        {
            return some.Content;
        }
    }
}