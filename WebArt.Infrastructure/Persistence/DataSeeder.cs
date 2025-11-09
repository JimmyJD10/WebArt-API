using System.Linq;
using WebArt.Domain.Entities;

namespace WebArt.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static void Seed(WebArtDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User { NombreCompleto = "Carlos Arteaga", Correo = "carlos@arte.com", PasswordHash = "12345", Rol = "artesano" },
                    new User { NombreCompleto = "Lucía Ramos", Correo = "lucia@cliente.com", PasswordHash = "12345", Rol = "cliente" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Nombre = "Cerámica", Slug = "ceramica" },
                    new Category { Nombre = "Textiles", Slug = "textiles" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }
    }
}