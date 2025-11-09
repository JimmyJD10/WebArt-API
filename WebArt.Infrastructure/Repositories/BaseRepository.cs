using Microsoft.EntityFrameworkCore;
using WebArt.Application.Interfaces;
using WebArt.Domain.Common;         
using WebArt.Infrastructure.Persistence;

namespace WebArt.Infrastructure.Repositories
{

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity 
    {
        protected readonly WebArtDbContext _context;

        public BaseRepository(WebArtDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            // Simplemente marca la entidad como modificada
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}