using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ShoppingEcommerce.Core.DomainModel.General
{
    public class Pagination<T> : List<T>, IPagination<T>
    {
        private IList<T> items;
        private int pageNumber;
        private int pageSize;
        private int totalItems;

        public Pagination(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
        {
           
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.totalItems = totalItems;
            this.items = items.ToList();
          
         
        }
        public int PageNumber
        {
            get { return pageNumber; }
        }

        public int PageSize
        {
            get { return pageSize; }
        }

        public int TotalItems
        {
            get { return totalItems; }
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling(((double)TotalItems) / PageSize); }
        }

        public int FirstItem
        {
            get { return ((PageNumber - 1) * PageSize) + 1; }
        }

        public int LastItem
        {
            get
            {
                return FirstItem + items.Count - 1;
            }
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPages; }
        }

        public IList<T> Items
        {
            get
            {
                return Items;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {

            foreach (var item in items)
            {
                yield return item;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }


    }
}
