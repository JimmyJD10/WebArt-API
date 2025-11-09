using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebArt.Domain.Entities;

namespace WebArt.Infrastructure.Persistence.Configurations
{
    public class ArtworkConfiguration : IEntityTypeConfiguration<Artwork> 
    {
        public void Configure(EntityTypeBuilder<Artwork> builder)
        {
            // 1. Mapea la entidad 'Artwork' a la tabla 'Products' de tu script
            builder.ToTable("Products"); 

            // 2. Configura la Clave Primaria (Guid)
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            // 3. Mapeo de Propiedades (¡La "Traducción"!)
            // (Columna de BD) <-- (Propiedad de Entidad)
            
            builder.Property(a => a.Title)
                .HasColumnName("Titulo") // Entidad usa 'Title', BD usa 'Titulo'
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(a => a.Description)
                .HasColumnName("Descripcion"); // Entidad usa 'Description', BD usa 'Descripcion'
            
            builder.Property(a => a.ImageUrl)
                .HasColumnName("Imagen"); // Entidad usa 'ImageUrl', BD usa 'Imagen'


            builder.Property(a => a.ArtistId)
                   .HasColumnName("UserId"); // Entidad usa 'ArtistId', BD usa 'UserId'

            builder.HasOne(a => a.Artist) // Propiedad de navegación 'Artist' en Artwork
                   .WithMany(u => u.Artworks) // Colección 'Artworks' en User
                   .HasForeignKey(a => a.ArtistId) // Clave foránea 'ArtistId' en Artwork
                   .HasConstraintName("FK_Products_User") // Nombre de la FK de tu script
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}