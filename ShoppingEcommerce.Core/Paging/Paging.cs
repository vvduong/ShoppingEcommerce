using ShoppingEcommerce.Core.Constants;
using System;
using System.Collections.Generic;

namespace ShoppingEcommerce.Core.Paging
{
    public class Paging<T>
    {
        public Paging() {

        }
        public Paging(IEnumerable<T> items,int pageNumber,int pageSize, int totalItems) {
            this.Page = new Page(totalItems, pageNumber, pageSize);
            this.Items = items;
        }
        public Page Page { get; set; }
        public IEnumerable<T> Items { get; set; }
        public IEnumerable<string> AdditionalInformation { get; set; }
    }
}