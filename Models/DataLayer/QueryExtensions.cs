using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyOptions<T>(
            this IQueryable<T> query, QueryOptions<T> options)
            where T : class
        {
            if (options.Where != null)
                query = query.Where(options.Where);

            if (options.OrderBy != null)
            {
                query = options.OrderByDescending
                    ? query.OrderByDescending(options.OrderBy)
                    : query.OrderBy(options.OrderBy);
            }

            foreach (var include in options.Includes)
                query = query.Include(include);

            return query;
        }
    }
}
