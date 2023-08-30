namespace TaskThinkland.Api.Entities;

public class JwtOption
{
    public required string SigningKey { get; set; }
    public required string ValidIssuer { get; set; }
    public required string ValidAudience { get; set; }
    public required int ExpiresInMinutes { get; set; }
}