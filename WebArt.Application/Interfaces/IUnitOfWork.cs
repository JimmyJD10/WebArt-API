namespace WebArt.Application.Interfaces;

using System;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Domain.Entities;

public interface IUnitOfWork : IDisposable
{
	IRepository<User> Users { get; }
	IRepository<Artwork> Artworks { get; }
	IRepository<Review> Reviews { get; }
	IRepository<Order> Orders { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}