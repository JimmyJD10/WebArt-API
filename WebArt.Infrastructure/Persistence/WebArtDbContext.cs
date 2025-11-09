using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebArt.Domain.Entities; 

namespace WebArt.Infrastructure.Persistence
{
    public class WebArtDbContext : DbContext
    {
        public WebArtDbContext(DbContextOptions<WebArtDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
      
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}