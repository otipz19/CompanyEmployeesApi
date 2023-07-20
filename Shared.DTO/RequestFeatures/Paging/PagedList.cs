using Microsoft.EntityFrameworkCore;

namespace Shared.DTO.RequestFeatures.Paging
{
    public class PagedList<T>
    {
        private readonly List<T> _items = new();

        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
        {
            MetaData = new PagingMetaData()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            };

            _items.AddRange(items);
        }

        public PagedList(IEnumerable<T> items, PagingMetaData metaData)
        {
            MetaData = metaData;
            _items.AddRange(items);
        }

        public PagingMetaData MetaData { get; set; }

        public IEnumerable<T> Items => _items;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query,
            BasePagingRequestParameters pagingParameters)
        {
            int totalCount = await query.CountAsync();
            List<T> items = await query
                .Skip((pagingParameters.CurrentPage - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync();
            return new PagedList<T>(items, pagingParameters.CurrentPage, pagingParameters.PageSize, totalCount);
        }
    }
}
