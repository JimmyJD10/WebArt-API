namespace WebArt.Application.Features.Users.Handlers;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebArt.Application.Features.Users.Commands;
using WebArt.Application.Services;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
	private readonly UserService _service;
	public DeleteUserHandler(UserService service) => _service = service;

	public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		await _service.DeleteAsync(request.Id, cancellationToken);
	}
}