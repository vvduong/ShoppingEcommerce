using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace ShoppingEcommerce.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbSet;

        public RepositoryBase(DbContext context)
        {

            this.context = context as DbContext;
            if (this.context != null)
            {
                this.dbSet = this.context.Set<TEntity>();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null)
        {
            total = this.Select(filter).Count();
            return this.Select(filter, orderBy, selectFields, includes, pageNumber, pageSize).ToList();
        }

        public IList<TEntity> GetAll(out int total,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            return this.GetAll(out total, filter, orderBy, null, includes, pageNumber, pageSize);
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            return this.Select(filter, orderBy, includes, null, null).ToList();
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.Select(filter, null, null, null, null).ToList();
        }
        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (selectFields != null)
            {
                foreach (var m_SelectField in selectFields)
                {
                    query.Select(m_SelectField);
                }
            }

            if (pageNumber != null && pageSize != null)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }



        public IList<TResult> GetByStoreProcedure<TResult>(string storeName, params object[] parameters)
        {
            var m_Method = this.context.GetType().GetMethod(storeName);
            var m_StoreResult = m_Method.Invoke(this.context, parameters) as ObjectResult<TResult>;

            return m_StoreResult.ToList();
        }

        public TResult ExecuteStoreProcedure<TResult>(string storeName, params object[] parameters)
        {
            var m_Method = this.context.GetType().GetMethod(storeName);
            return (TResult)m_Method.Invoke(this.context, parameters);
        }



        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return dbSet;
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
        {
            return ((IObjectContextAdapter)context).ObjectContext.ExecuteFunction<TElement>(functionName,parameters);
        }
    }
}
