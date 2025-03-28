using Microsoft.EntityFrameworkCore;

namespace FlightManager.Utilities
{
    public class PaginatedList<T> : List<T>
    {
        public int Page { get; }

        public int TotalPages { get; }

        public PaginatedList(List<T> items, int count, int page, int pageSize)
        {
            this.Page = page;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage => this.Page > 1;

        public bool HasNextPage => this.Page < this.TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize)
        {
            var count = await source.CountAsync();

            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<T>(items, count, page, pageSize);
        }
    }

}
