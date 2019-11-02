using System.Collections.Generic;

namespace ShoppingEcommerce.Specifications
{
    public sealed class SpecificationBuilder<TEntity> : IOrderedSpecification<TEntity> where TEntity : class
    {
        /// <summary>
        /// </summary>
        /// <param name="querySpecification"></param>
        public SpecificationBuilder(IQuerySpecification<TEntity> querySpecification)
        {
            QuerySpecification = querySpecification;

            OrderSpecifications = new List<IOrderSpecification<TEntity>>();
        }

        /// <summary>
        /// </summary>
        /// <param name="orderSpecification"></param>
        public SpecificationBuilder(IOrderSpecification<TEntity> orderSpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>>
            {
                orderSpecification
            };
        }

        public IQuerySpecification<TEntity> QuerySpecification { get; set; }

        public List<IOrderSpecification<TEntity>> OrderSpecifications { get; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public SpecificationBuilder<TEntity> Internal => this;
    }
}