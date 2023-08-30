using TaskThinkland.Api.Entities;

namespace TaskThinkland.Api.Services.Options;

public interface IJwtService
{
    (string, double) GenerateToken(User user);
}