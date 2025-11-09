namespace WebArt.Application.Features.Users.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Application.Features.Users.Commands;
using WebArt.Application.Services;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
	private readonly UserService _service;
	public CreateUserHandler(UserService service) => _service = service;

	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		await _service.CreateAsync(request.User, cancellationToken);
	}
}