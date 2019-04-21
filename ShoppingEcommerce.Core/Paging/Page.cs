using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingEcommerce.Core.Paging
{
    public class Page
    {
        public const int DefaultPageSize = 9;
        public const int DefaultTakePage = 2;

        public Page(int totalItems, int? page, int pageSize = DefaultPageSize, int takePage = DefaultTakePage)
        {
            var currentPage = 0;
            if (page == null || page == 0) currentPage = 1;
            else currentPage = (int)page;

            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            if (totalPages > 0 && page > totalPages) currentPage = totalPages;

            var startPage = currentPage - takePage;
            var endPage = currentPage + takePage;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > (takePage * 2) + 1)
                    startPage = endPage - takePage * 2;
            }
            var offsetItems = (currentPage - 1) * pageSize;
            var summary = string.Format("{0}-{1} ({2})",
                offsetItems + 1,
                totalItems > pageSize && (offsetItems + pageSize) < totalItems ? offsetItems + pageSize : totalItems,
                totalItems);

            TotalItems = totalItems;
            OffsetItems = offsetItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            Summary = summary;
        }

        public int TotalItems { get; private set; }
        public int OffsetItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public string Summary { get; private set; }
    }
}