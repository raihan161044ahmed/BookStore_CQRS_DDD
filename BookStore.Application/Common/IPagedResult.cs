using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common
{
    public interface IPagedResult<T>
    {
        IReadOnlyList<T> Items { get; }
        int TotalCount { get; }
        int Page { get; }
        int PageSize { get; }
    }


    public class PagedResult<T> : IPagedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }


        public PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }
}
