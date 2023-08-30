namespace TaskThinkland.Api.DTOs.UserDTOs;

public class UserTokenDto
{
    public required string Username { get; set; }
    public required double ExpiresInMinutes { get; set; }
    public required string Token { get; set; }
}