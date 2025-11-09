namespace WebArt.Application.Features.Users.Commands;

using MediatR;
using WebArt.Application.DTOs;

public class UpdateUserCommand : IRequest
{
	public UserDto User { get; set; } = null!;
}