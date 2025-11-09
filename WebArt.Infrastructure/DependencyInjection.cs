using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebArt.Application.Interfaces;
using WebArt.Infrastructure.Security;

namespace WebArt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetRequiredSection("Jwt"));
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtService, JwtService>();
        return services;
    }
}