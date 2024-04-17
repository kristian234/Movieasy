using Microsoft.EntityFrameworkCore;

namespace Movieasy.Application.Common
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int totalPages)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public List<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalPages { get; }

        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> query,
            int page,
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, page, pageSize, totalPages);
        }
    }
}
