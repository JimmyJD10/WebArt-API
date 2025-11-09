using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebArt.Application.Interfaces; // <-- Interfaz de Application
using WebArt.Infrastructure.Persistence;
using WebArt.Infrastructure.UnitOfWork; // <-- Tu implementación

namespace WebArt.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            // 1. Configurar el DbContext (SQLite)
            services.AddDbContext<WebArtDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(WebArtDbContext).Assembly.FullName)));

            // 2. Registrar tu UnitOfWork
            // Cuando un servicio (UserService) pida IUnitOfWork, 
            // el sistema le entregará tu implementación (UnitOfWork).
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            
            // NOTA: No necesitamos registrar IRepository<> porque el UnitOfWork
            // es el único que los crea y los entrega.
            
            // (Aquí también irían los servicios de Integrante 4: JWT, PasswordHasher, etc.)

            return services;
        }
    }
}