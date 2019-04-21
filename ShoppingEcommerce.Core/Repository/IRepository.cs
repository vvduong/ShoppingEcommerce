using ShoppingEcommerce.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll(Expression<Func<T, bool>> expression);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        IList<T> GetAll(string orderBy, params Expression<Func<T, bool>>[] searchCondition);
        Task<IList<T>> GetAllAsync(string orderBy, params Expression<Func<T, bool>>[] searchCondition);
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        bool CheckExists(Expression<Func<T, bool>> condition);
        Task<bool> CheckExistsAsync(Expression<Func<T, bool>> condition);
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
        PagedList<T> Get(int? pageIndex, int? pageSize, string orderBy, params Expression<Func<T, bool>>[] searchCondition);
        Task<PagedList<T>> GetAsync(int? pageIndex, int? pageSize, string orderBy, params Expression<Func<T, bool>>[] searchCondition);
        Task<PagedList<T>> GetAsync(int? pageIndex, int? pageSize, params Expression<Func<T, bool>>[] searchCondition);
        PagedList<T> Get(int? pageIndex, int? pageSize, params Expression<Func<T, bool>>[] searchCondition);
        PagedList<T> Get(int? pageIndex, int? pageSize);
        Task<PagedList<T>> GetAsync(int? pageIndex, int? pageSize);
        void Insert(T entity);
        void Insert(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IList<T> entities);
        void Detached(T entity);
        void Attach(T entity);
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetQueryable();
    }
}
