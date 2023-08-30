using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskThinkland.Api.PaginationModels;

namespace TaskThinkland.Api.Extensions;

public static class QueryableExtensions
{
    public static async Task<IEnumerable<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source,
        HttpContextHelper httpContextHelper, PaginationParams filterParams)
    {
        var content = JsonConvert.SerializeObject(
            new PaginationMetaData(source.Count(), filterParams.AmountData, filterParams.PageNumber));

        httpContextHelper.AddResponseToHeaderData("X-Pagination", content);

        return await source.Skip(filterParams.AmountData * (filterParams.PageNumber - 1)).Take(filterParams.AmountData).ToListAsync();
    }
}