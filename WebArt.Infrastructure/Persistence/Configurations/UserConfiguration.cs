using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebArt.Domain.Entities;
using WebArt.Domain.Enums; // Asegúrate de importar tus Enums

namespace WebArt.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            // --- Mapeo de Propiedades (La "Traducción") ---

            // Entidad usa 'Name', BD (script) usa 'NombreCompleto'
            builder.Property(u => u.Name) 
                .HasColumnName("NombreCompleto")
                .IsRequired()
                .HasMaxLength(200);
            
            // --- ¡AQUÍ ESTÁ LA CORRECCIÓN! ---
            // La entidad NO tiene 'Email'. Tiene 'EmailValue'.
            builder.Property(u => u.EmailValue) 
                .HasColumnName("Correo") // Mapea a la columna 'Correo' de tu script
                .IsRequired()
                .HasMaxLength(200);

            // Crea el índice único en la columna 'Correo' (que mapea a 'EmailValue')
            builder.HasIndex(u => u.EmailValue).IsUnique();
            


            builder.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired()
                .HasMaxLength(512);

            // Entidad usa 'Biography', BD (script) usa 'Descripcion'
            builder.Property(u => u.Biography) 
                .HasColumnName("Descripcion"); 

            // Entidad usa 'ProfileImageUrl', BD (script) usa 'FotoPerfil'
            builder.Property(u => u.ProfileImageUrl)
                .HasColumnName("FotoPerfil");

            // Mapeo de Enum (RoleType) a String (Rol)
            builder.Property(u => u.Role)
                .HasColumnName("Rol")
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue(RoleType.Client) // O como se llame tu rol por defecto
                .HasConversion<string>(); // Convierte Enum a "Client", "Artist", etc.



            builder.Navigation(u => u.Artworks)
                .HasField("_artworks")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                
            builder.Navigation(u => u.OrdersAsCustomer)
                .HasField("_ordersAsCustomer")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                
            builder.Navigation(u => u.OrdersAsArtist)
                .HasField("_ordersAsArtist")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.ReviewsGiven)
                .HasField("_reviewsGiven")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                
            builder.Navigation(u => u.ReviewsReceived)
                .HasField("_reviewsReceived")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                
            builder.Navigation(u => u.SentMessages)
                .HasField("_sentMessages")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                
            builder.Navigation(u => u.ReceivedMessages)
                .HasField("_receivedMessages")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}