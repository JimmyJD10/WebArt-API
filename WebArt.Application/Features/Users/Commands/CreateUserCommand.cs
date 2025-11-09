namespace WebArt.Application.Features.Users.Commands;

using MediatR;
using WebArt.Application.DTOs;

public class CreateUserCommand : IRequest
{
	public UserDto User { get; set; } = null!;
}