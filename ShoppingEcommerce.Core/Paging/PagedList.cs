using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Core.Paging
{
    public class PagedList<T> where T : class
    {
        public PagedList()
        {
            Items = new List<T>();
        }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling(TotalItems * 1.0 / PageSize);
            }
        }
        public int TotalItems { set; get; }
        public IList<T> Items { set; get; }

        public PagedList<T> ToPage(IQueryable<T> queryable, int? pageIndex, int? pageSize, string orderBy)
        {
            if (!pageIndex.HasValue)
                throw new ArgumentNullException($"pageIndex={pageIndex}: Invalid");
            if (!pageSize.HasValue)
                throw new ArgumentNullException($"pageSize={pageSize}: Invalid");
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException($"orderBy={orderBy}: Invalid");
            queryable = queryable.OrderBy(orderBy);
            TotalItems = queryable.Count();
            PageIndex = pageIndex.Value;
            PageSize = pageSize.Value;
            int skip = (PageIndex - 1) * PageSize;
            int take = PageSize;
            Items = queryable.Skip(skip).Take(take).ToList();
            return this;
        }
        public PagedList<T> ToPage(IQueryable<T> queryable, int? pageIndex, int? pageSize)
        {
            if (!pageIndex.HasValue)
                throw new ArgumentNullException($"pageIndex={pageIndex}: Invalid");
            TotalItems = queryable.Count();
            PageIndex = pageIndex.Value;
            PageSize = pageSize ?? TotalItems;
            int skip = (PageIndex - 1) * PageSize;
            int take = PageSize;
            Items = queryable.Skip(skip).Take(take).ToList();
            return this;
        }

        public async Task<PagedList<T>> ToPageAsync(IQueryable<T> queryable, int? pageIndex, int? pageSize, string orderBy)
        {
            if (!pageIndex.HasValue)
                throw new ArgumentNullException($"pageIndex={pageIndex}: Invalid");
            if (!pageSize.HasValue)
                throw new ArgumentNullException($"pageSize={pageSize}: Invalid");
            if (string.IsNullOrEmpty(orderBy))
                throw new ArgumentNullException($"orderBy={orderBy}: Invalid");
            queryable = queryable.OrderBy(orderBy);
            TotalItems = await queryable.CountAsync();
            PageIndex = pageIndex.Value;
            PageSize = pageSize.Value;
            int skip = (PageIndex - 1) * PageSize;
            int take = PageSize;
            Items = await queryable.Skip(skip).Take(take).ToListAsync();
            return this;
        }
        public async Task<PagedList<T>> ToPageAsync(IQueryable<T> queryable, int? pageIndex, int? pageSize)
        {
            if (!pageIndex.HasValue)
                throw new ArgumentNullException($"pageIndex={pageIndex}: Invalid");
            if (!pageSize.HasValue)
                throw new ArgumentNullException($"pageSize={pageSize}: Invalid");
            TotalItems = await queryable.CountAsync();
            PageIndex = pageIndex.Value;
            PageSize = pageSize.Value;
            int skip = (PageIndex - 1) * PageSize;
            int take = PageSize;
            Items = await queryable.Skip(skip).Take(take).ToListAsync();
            return this;
        }
    }
}
