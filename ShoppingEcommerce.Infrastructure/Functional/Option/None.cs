namespace ShoppingEcommerce.Functional.Option
{
    public class None<T> : Option<T>
    {
    }

    public class None
    {
        private None()
        {
        }

        public static None Value => new None();
    }
}