using ShoppingEcommerce.Core.Paging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Core.Repository
{
    public abstract class Repository<T> where T : class
    {
        const string OrderBy = "Id";
        protected DbContext Context;
        private DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }

        public virtual IList<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }
        public virtual IList<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).AsNoTracking().ToList();
        }
        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }


        public virtual IList<T> GetAll(string orderBy, params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy(orderBy);
            return queryable.ToList();
        }
        public virtual async Task<IList<T>> GetAllAsync(string orderBy, params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy(orderBy);
            return await queryable.ToListAsync(); 
        }
        public async virtual Task<IList<T>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public virtual T GetById(Guid id)
        {
            var entity = dbSet.Find(id);
            return entity;
        }
        public virtual T Get(Expression<Func<T, bool>> expression)
        {
            var entity = dbSet.AsNoTracking().Single(expression);
            return entity;
        }
        public async virtual Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.AsNoTracking().SingleAsync(expression);
            return entity;
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }
        public virtual PagedList<T> Get(int? pageIndex, int? pageSize, string orderBy, params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy(orderBy);
            PagedList<T> pagedList = new PagedList<T>();
            return pagedList.ToPage(queryable, pageIndex, pageSize);
        }
        public virtual async Task<PagedList<T>> GetAsync( int? pageIndex, int? pageSize, string orderBy, params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy(orderBy);
            PagedList<T> pagedList = new PagedList<T>();
            return await pagedList.ToPageAsync(queryable, pageIndex, pageSize);
        }
        public virtual async Task<PagedList<T>> GetAsync(int? pageIndex, int? pageSize,params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy("Id");
            PagedList<T> pagedList = new PagedList<T>();
            return await pagedList.ToPageAsync(queryable, pageIndex, pageSize);
        }
        public virtual PagedList<T> Get(int? pageIndex, int? pageSize, params Expression<Func<T, bool>>[] searchCondition)
        {
            if (searchCondition == null)
                throw new ArgumentException("searchCondition can not null");
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            foreach (var condition in searchCondition)
            {
                queryable = queryable.Where(condition);
            }
            queryable = queryable.OrderBy(OrderBy);
            PagedList<T> pagedList = new PagedList<T>();
            return pagedList.ToPage(queryable, pageIndex, pageSize);
        }
        public virtual PagedList<T> Get(int? pageIndex, int? pageSize)
        {
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            queryable = queryable.OrderBy(OrderBy);
            PagedList<T> pagedList = new PagedList<T>();
            return pagedList.ToPage(queryable, pageIndex, pageSize);
        }
        public virtual Task<PagedList<T>> GetAsync(int? pageIndex, int? pageSize)
        {
            IQueryable<T> queryable = dbSet.AsNoTracking().AsQueryable();
            queryable = queryable.OrderBy(OrderBy);
            PagedList<T> pagedList = new PagedList<T>();
            return pagedList.ToPageAsync(queryable, pageIndex, pageSize);
        }
        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity can not null");
            dbSet.Add(entity);
        }
        public virtual void Insert(IList<T> entities)
        {
            if (entities == null)
                throw new ArgumentException("entities can not null");
            dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity can not null");
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Detached(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity can not null");
            Context.Entry(entity).State = EntityState.Detached;
        }
        public virtual void Attach(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity can not null");
            dbSet.Attach(entity);
        }
        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity can not null");
            dbSet.Remove(entity);
        }
        public virtual void Delete(IList<T> entities)
        {
            if (entities == null)
                throw new ArgumentException("entities can not null");
            dbSet.RemoveRange(entities);
        }
        public virtual bool CheckExists(Expression<Func<T, bool>> condition)
        {
            return dbSet.Any(condition);
        }
        public async virtual Task<bool> CheckExistsAsync(Expression<Func<T, bool>> condition)
        {
            return await dbSet.AnyAsync(condition);
        }

        public virtual IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = Context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = Context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}
