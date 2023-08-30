using Microsoft.EntityFrameworkCore;
using TaskThinkland.Api.Context;

namespace TaskThinkland.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddThinklanDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch(switchName: "Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);
        services.AddDbContext<AppDbContext>(config =>
        {
            config.UseSnakeCaseNamingConvention()
               // .UseInMemoryDatabase("ProductDb");
            .UseNpgsql(configuration.GetConnectionString("ProductDb"));
        });
    }
}