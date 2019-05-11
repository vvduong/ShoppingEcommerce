using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity GetByID(object entityID);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        IList<TEntity> GetAll();
        IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null);
        IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IList<TResult> GetByStoreProcedure<TResult>(string storeName, params object[] parameters);
        TResult ExecuteStoreProcedure<TResult>(string storeName, params object[] parameters);

        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters);

        IQueryable<TEntity> GetQueryable();

        int Count(Expression<Func<TEntity, bool>> filter);
        int Count();
    }
}
