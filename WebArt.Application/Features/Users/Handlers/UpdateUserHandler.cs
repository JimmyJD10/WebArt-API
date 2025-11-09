namespace WebArt.Application.Features.Users.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Application.Features.Users.Commands;
using WebArt.Application.Services;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
	private readonly UserService _service;
	public UpdateUserHandler(UserService service) => _service = service;

	public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		await _service.UpdateAsync(request.User, cancellationToken);
	}
}