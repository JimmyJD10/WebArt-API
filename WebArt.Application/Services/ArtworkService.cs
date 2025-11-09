namespace WebArt.Application.Services;

using AutoMapper;
using WebArt.Application.DTOs;
using WebArt.Application.Interfaces;
using WebArt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class ArtworkService
{
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;

	public ArtworkService(IUnitOfWork uow, IMapper mapper)
	{
		_uow = uow;
		_mapper = mapper;
	}

	public async Task<ArtworkDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var a = await _uow.Artworks.GetByIdAsync(id, cancellationToken);
		return a is null ? null : _mapper.Map<ArtworkDto>(a);
	}

	public async Task<IEnumerable<ArtworkDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var list = await _uow.Artworks.GetAllAsync(cancellationToken);
		return _mapper.Map<IEnumerable<ArtworkDto>>(list);
	}

	public async Task CreateAsync(ArtworkDto dto, CancellationToken cancellationToken = default)
	{
		var entity = _mapper.Map<Artwork>(dto);
		await _uow.Artworks.AddAsync(entity, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);
	}
}