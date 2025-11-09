namespace WebArt.Application.Features.Users.Handlers;

using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Application.DTOs;
using WebArt.Application.Features.Users.Queries;
using WebArt.Application.Services;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
	private readonly UserService _service;
	public GetAllUsersHandler(UserService service) => _service = service;

	public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		return await _service.GetAllAsync(cancellationToken);
	}
}