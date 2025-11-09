namespace WebArt.Application.Services;

using AutoMapper;
using WebArt.Application.DTOs;
using WebArt.Application.Interfaces;
using WebArt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class UserService
{
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;

	public UserService(IUnitOfWork uow, IMapper mapper)
	{
		_uow = uow;
		_mapper = mapper;
	}

	public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var user = await _uow.Users.GetByIdAsync(id, cancellationToken);
		return user is null ? null : _mapper.Map<UserDto>(user);
	}

	public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var users = await _uow.Users.GetAllAsync(cancellationToken);
		return _mapper.Map<IEnumerable<UserDto>>(users);
	}

	public async Task CreateAsync(UserDto dto, CancellationToken cancellationToken = default)
	{
		var entity = _mapper.Map<User>(dto);
		await _uow.Users.AddAsync(entity, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(UserDto dto, CancellationToken cancellationToken = default)
	{
		var existing = await _uow.Users.GetByIdAsync(dto.Id, cancellationToken);
		if (existing is null) throw new InvalidOperationException("User not found");
		_mapper.Map(dto, existing);
		_uow.Users.Update(existing);
		await _uow.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var existing = await _uow.Users.GetByIdAsync(id, cancellationToken);
		if (existing is null) throw new InvalidOperationException("User not found");
		_uow.Users.Delete(existing);
		await _uow.SaveChangesAsync(cancellationToken);
	}
}