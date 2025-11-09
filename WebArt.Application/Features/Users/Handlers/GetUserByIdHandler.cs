namespace WebArt.Application.Features.Users.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Application.DTOs;
using WebArt.Application.Features.Users.Queries;
using WebArt.Application.Services;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
	private readonly UserService _service;
	public GetUserByIdHandler(UserService service) => _service = service;

	public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		return await _service.GetByIdAsync(request.Id, cancellationToken);
	}
}