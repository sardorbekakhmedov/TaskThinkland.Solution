using TaskThinkland.Api.PaginationModels;

namespace TaskThinkland.Api.Filters;

public class ProductFilter : PaginationParams
{
    public string? Title { get; set; }
    public decimal? FromPrice { get; set; }
    public decimal? ToPrice { get; set; }
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
}          