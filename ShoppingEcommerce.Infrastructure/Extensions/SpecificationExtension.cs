using System;
using System.Linq;
using System.Linq.Expressions;
using ShoppingEcommerce.Specifications;

namespace ShoppingEcommerce.Extensions
{
    public static class SpecificationExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="querySpecification"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> And<TEntity>(this ISpecificationBuilder<TEntity> specification
            , IQuerySpecification<TEntity> querySpecification) where TEntity : class
        {
            return SetQuerySpecification(specification
                , And(specification.Internal.QuerySpecification.AsExpression()
                    , querySpecification.AsExpression()));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="querySpecification"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Or<TEntity>(this ISpecificationBuilder<TEntity> specification
            , IQuerySpecification<TEntity> querySpecification) where TEntity : class
        {
            return SetQuerySpecification(specification
                , Or(specification.Internal.QuerySpecification.AsExpression()
                    , querySpecification.AsExpression()));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Not<TEntity>(this ISpecificationBuilder<TEntity> specification)
            where TEntity : class
        {
            return SetQuerySpecification(specification
                , Not(specification.Internal.QuerySpecification.AsExpression()));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsSatisfiedBy<TEntity>(this ISpecificationBuilder<TEntity> specification
            , TEntity entity) where TEntity : class
        {
            return specification.Internal.QuerySpecification.AsFunc()(entity);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specificationBuilder"></param>
        /// <param name="querySpecification"></param>
        /// <returns></returns>
        private static ISpecificationBuilder<TEntity>
            SetQuerySpecification<TEntity>(ISpecificationBuilder<TEntity> specificationBuilder
                , IQuerySpecification<TEntity> querySpecification) where TEntity : class
        {
            var specificationBuilderInternal = specificationBuilder.Internal;

            specificationBuilderInternal.QuerySpecification = querySpecification;

            return specificationBuilderInternal;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="leftExpression"></param>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        private static Specification<TEntity> And<TEntity>(Expression<Func<TEntity, bool>> leftExpression
            , Expression<Func<TEntity, bool>> rightExpression) where TEntity : class
        {
            return new Specification<TEntity>(PredicateBuilder
                .New(leftExpression)
                .And(rightExpression));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="leftExpression"></param>
        /// <param name="rightExpression"></param>
        /// <returns></returns>
        private static Specification<TEntity> Or<TEntity>(Expression<Func<TEntity, bool>> leftExpression
            , Expression<Func<TEntity, bool>> rightExpression) where TEntity : class
        {
            return new Specification<TEntity>(PredicateBuilder
                .New(leftExpression)
                .Or(rightExpression));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static Specification<TEntity> Not<TEntity>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(Expression.Not(expression.Body)
                , expression.Parameters));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        public static IOrderedSpecification<TEntity> OrderBy<TEntity>(this ISpecificationBuilder<TEntity> specification
            , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            return AddOrderSpecification(specification
                , orderSpecification);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderSpecification<TEntity> specification
            , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            return AddOrderSpecification(specification
                , orderSpecification);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        public static IOrderedSpecification<TEntity> ThenBy<TEntity>(this IOrderedSpecification<TEntity> specification
            , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            return AddOrderSpecification(specification
                , orderSpecification);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="specificationBuilder"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity>
            UseOrdering<TEntity>(this ISpecificationBuilder<TEntity> specification
                , ISpecificationBuilder<TEntity> specificationBuilder) where TEntity : class
        {
            var specificationInternal = specification.Internal;
            var specificationBuilderInternal = specificationBuilder.Internal;

            specificationInternal.OrderSpecifications.AddRange(specificationBuilderInternal.OrderSpecifications);

            specificationInternal.Skip = specificationBuilderInternal.Skip;
            specificationInternal.Take = specificationBuilderInternal.Take;

            return specificationInternal;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Skip<TEntity>(this ISpecificationBuilder<TEntity> specification
            , int count) where TEntity : class
        {
            var specificationInternal = specification.Internal;

            specificationInternal.Skip = count;

            return specificationInternal;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Take<TEntity>(this ISpecificationBuilder<TEntity> specification
            , int count) where TEntity : class
        {
            var specificationInternal = specification.Internal;

            specificationInternal.Take = count;

            return specificationInternal;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<TEntity> Paginate<TEntity>(this ISpecificationBuilder<TEntity> specification
            , int pageNumber
            , int pageSize) where TEntity : class
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Cannot be less than 1.", nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Cannot be less than 1.", nameof(pageSize));
            }

            var specificationInternal = specification.Internal;

            specificationInternal.Skip = (pageNumber - 1) * pageSize;
            specificationInternal.Take = pageSize;

            return specificationInternal;
        }

        public static ISpecificationBuilder<TEntity> Paginate<TEntity>(this ISpecificationBuilder<TEntity> specification
            , int skip, int take, int total) where TEntity : class
        {
            if (skip < 0)
            {
                throw new ArgumentException("Cannot be less than 0.", nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException("Cannot be less than 1.", nameof(take));
            }

            if (skip + take > total)
            {
                take = total - skip;
            }

            var specificationInternal = specification.Internal;

            specificationInternal.Skip = skip;
            specificationInternal.Take = take;

            return specificationInternal;
        }

        public static bool IsOrdered<TEntity>(this ISpecificationBuilder<TEntity> specification) where TEntity : class
        {
            return specification.Internal.OrderSpecifications.Any();
        }

        public static bool HasSkip<TEntity>(this ISpecificationBuilder<TEntity> specification) where TEntity : class
        {
            return specification.Internal.Skip != null;
        }

        public static bool HasTake<TEntity>(this ISpecificationBuilder<TEntity> specification) where TEntity : class
        {
            return specification.Internal.Take != null;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="specification"></param>
        /// <param name="orderSpecification"></param>
        /// <returns></returns>
        private static IOrderedSpecification<TEntity>
            AddOrderSpecification<TEntity>(ISpecificationBuilder<TEntity> specification
                , IOrderSpecification<TEntity> orderSpecification) where TEntity : class
        {
            var specificationInternal = specification.Internal;

            specificationInternal.OrderSpecifications.Add(orderSpecification);

            return specificationInternal;
        }
    }
}