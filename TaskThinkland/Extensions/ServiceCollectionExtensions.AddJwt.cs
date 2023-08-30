using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskThinkland.Api.Entities;

namespace TaskThinkland.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddJwtValidation(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOption>(configuration.GetSection("JwtBearer"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var configString = configuration["JwtBearer:SigningKey"];
                var validIssuer = configuration["JwtBearer:ValidIssuer"];
                var validAudience = configuration["JwtBearer:ValidAudience"];

                if (configString is null || validIssuer is null || validAudience is null)
                    throw new ArgumentNullException(nameof(configString));

                var signingKey = Encoding.UTF8.GetBytes(configString);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });
    }
}