using TaskThinkland.Api.PaginationModels;

namespace TaskThinkland.Api.Filters;

public class UserFilter : PaginationParams
{
    public string? Username { get; set; }
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
}