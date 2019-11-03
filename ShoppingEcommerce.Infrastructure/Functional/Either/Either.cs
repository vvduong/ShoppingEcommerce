namespace ShoppingEcommerce.Infrastructure.Functional.Either
{
    public abstract class Either<TLeft, TRight>
    {
        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        public static implicit operator Either<TLeft, TRight>(TLeft left)
        {
            return new Left<TLeft, TRight>(left);
        }

        /// <summary>
        /// </summary>
        /// <param name="right"></param>
        public static implicit operator Either<TLeft, TRight>(TRight right)
        {
            return new Right<TLeft, TRight>(right);
        }
    }
}