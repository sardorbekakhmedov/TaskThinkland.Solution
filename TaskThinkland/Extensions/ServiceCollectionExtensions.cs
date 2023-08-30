using TaskThinkland.Api.Context.Repositories.GenericRepositories;
using TaskThinkland.Api.Mappers;
using TaskThinkland.Api.PaginationModels;
using TaskThinkland.Api.Services.FileServices;
using TaskThinkland.Api.Services.Options;
using TaskThinkland.Api.Services.ProductServices;
using TaskThinkland.Api.Services.UserService;

namespace TaskThinkland.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddThinklandServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddAutoMapper(typeof(MapperProfile));
        services.AddScoped<HttpContextHelper>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();


    }
}