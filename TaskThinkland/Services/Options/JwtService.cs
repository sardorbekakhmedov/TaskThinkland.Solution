using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskThinkland.Api.Entities;

namespace TaskThinkland.Api.Services.Options;

public class JwtService : IJwtService
{
    private readonly JwtOption _options;

    public JwtService(IOptions<JwtOption> options)
    {
        _options = options.Value;
    }

    public (string, double) GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name , user.Username),
        };

        var signingKey = Encoding.UTF8.GetBytes(_options.SigningKey);
        var symmetricKey = new SymmetricSecurityKey(signingKey);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: _options.ValidIssuer,
            audience: _options.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiresInMinutes),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        var expiresMinutes = TimeSpan.FromMinutes(_options.ExpiresInMinutes).TotalMinutes;

        return new(token, expiresMinutes);
    }
}