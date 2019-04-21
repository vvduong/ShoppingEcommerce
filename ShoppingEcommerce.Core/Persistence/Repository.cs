using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ShoppingEcommerce.Mapper;
using Unity;


namespace ShoppingEcommerce.Core.Persistence
{
    public abstract class Repository<TDbContext, TEntity> where TDbContext : DbContext where TEntity : class
    {
        private readonly AmbientDbContextLocator _ambientDbContextLocator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ambientDbContextLocator"></param>
        protected Repository(AmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        protected DbContext DbContext
            => _ambientDbContextLocator.Get<TDbContext>()
               ?? throw new InvalidOperationException($"No ambient DbContext of type {nameof(TDbContext)} found. " + @"
                    This means that this repository method has been called outside of the scope of a DbContextScope. 
                    A repository must only be accessed within the scope of a DbContextScope, 
                    which takes care of creating the DbContext instances 
                    that the repositories need and making them available as ambient contexts. 
                    This is what ensures that, for any given DbContext-derived type, 
                    the same instance is used throughout the duration of a business transaction. 
                    To fix this issue, use IDbContextScopeFactory in your top-level business logic service method 
                    to create a DbContextScope that wraps the entire business transaction that your service method implements. 
                    Then access this repository within that scope. 
                    Refer to the comments in the DbContextScope.cs file for more details.");

        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        [Dependency] protected IMapper Mapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbContext.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Modify(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryNoTrackingInclude(predicate, includes).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public TResult Find<TResult>(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes) where TResult : IMapping
        {
            return QueryNoTrackingInclude(predicate, includes)
                .ProjectToSingleOrDefault<TResult>(Mapper.ConfigurationProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public TEntity FindWithTracking(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryWithTrackingInclude(predicate, includes).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IReadOnlyList<TEntity> Get(params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryNoTrackingInclude(includes).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IReadOnlyList<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryNoTrackingInclude(predicate, includes).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IReadOnlyList<TResult> Get<TResult>(params Expression<Func<TEntity, object>>[] includes)
            where TResult : IMapping
        {
            return QueryNoTrackingInclude(includes).ProjectToList<TResult>(Mapper.ConfigurationProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IReadOnlyList<TResult> Get<TResult>(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes) where TResult : IMapping
        {
            return QueryNoTrackingInclude(predicate, includes).ProjectToList<TResult>(Mapper.ConfigurationProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IReadOnlyList<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(sql, parameters).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IReadOnlyList<TResult> SqlQuery<TResult>(string sql, params object[] parameters) where TResult : class
        {
            return DbContext.Database.SqlQuery<TResult>(sql, parameters).ToList();
        }

        public int Count()
        {
            return QueryNoTracking().Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryNoTracking(predicate).Count();
        }

        private IQueryable<TEntity> QueryNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryNoTracking().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryNoTrackingInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryInclude(QueryNoTracking(), includes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryNoTrackingInclude(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryInclude(QueryNoTracking(predicate), includes);
        }

        private IQueryable<TEntity> QueryWithTracking()
        {
            return DbSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryWithTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryWithTracking().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryWithTrackingInclude(Expression<Func<TEntity, bool>> predicate
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return QueryInclude(QueryWithTracking(predicate), includes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        private static IQueryable<TEntity> QueryInclude(IQueryable<TEntity> query
            , params Expression<Func<TEntity, object>>[] includes)
        {
            return includes == null ? query : includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}