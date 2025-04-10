using System.Linq.Expressions;

namespace MyWebApi.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationParams paginationParams)
    {
        return query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize);
    }

    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, PaginationParams paginationParams, Expression<Func<T, bool>> filterExpression)
    {
        if (!string.IsNullOrWhiteSpace(paginationParams.Search))
        {
            query = query.Where(filterExpression);
        }

        return query;
    }
}