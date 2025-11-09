using WebArt.Application.Interfaces; // <-- Interfaz de Application
using WebArt.Domain.Entities;       // <-- Entidades de Domain
using WebArt.Infrastructure.Persistence;
using WebArt.Infrastructure.Repositories;

namespace WebArt.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebArtDbContext _context;
        
        // Almacenamiento "lazy" para los repositorios
        private IRepository<User>? _users;
        private IRepository<Artwork>? _artworks;
        private IRepository<Review>? _reviews;
        private IRepository<Order>? _orders;

        public UnitOfWork(WebArtDbContext context)
        {
            _context = context;
        }

        // Implementación de la interfaz IUnitOfWork
        // Crea una instancia del repositorio genérico si aún no existe
        public IRepository<User> Users => 
            _users ??= new BaseRepository<User>(_context);

        public IRepository<Artwork> Artworks => 
            _artworks ??= new BaseRepository<Artwork>(_context);
            
        public IRepository<Review> Reviews => 
            _reviews ??= new BaseRepository<Review>(_context);

        public IRepository<Order> Orders => 
            _orders ??= new BaseRepository<Order>(_context);
        
        // (Agrega los otros repositorios que IUnitOfWork te pida)


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Esta es la función que Application llamará
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}