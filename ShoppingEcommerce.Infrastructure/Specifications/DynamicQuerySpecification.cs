namespace ShoppingEcommerce.Specifications
{
    public abstract class DynamicSpecification<TEntity, TValue> : QuerySpecification<TEntity>
        , IDynamicQuerySpecification<TEntity, TValue> where TEntity : class
    {
        protected DynamicSpecification()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        protected DynamicSpecification(TValue value)
        {
            Set(value);
        }

        public TValue Value { get; private set; }

        public IDynamicQuerySpecification<TEntity, TValue> Set(TValue value)
        {
            Value = value;

            return this;
        }
    }
}