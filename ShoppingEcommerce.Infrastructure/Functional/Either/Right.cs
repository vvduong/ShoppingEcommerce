namespace ShoppingEcommerce.Infrastructure.Functional.Either
{
    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        /// <summary>
        /// </summary>
        /// <param name="content"></param>
        public Right(TRight content)
        {
            Content = content;
        }

        private TRight Content { get; }

        /// <summary>
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator TRight(Right<TLeft, TRight> right)
        {
            return right.Content;
        }
    }
}