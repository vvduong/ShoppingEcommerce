namespace ShoppingEcommerce.Infrastructure.Functional.Either
{
    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        /// <summary>
        /// </summary>
        /// <param name="content"></param>
        public Left(TLeft content)
        {
            Content = content;
        }

        private TLeft Content { get; }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        public static implicit operator TLeft(Left<TLeft, TRight> left)
        {
            return left.Content;
        }
    }
}