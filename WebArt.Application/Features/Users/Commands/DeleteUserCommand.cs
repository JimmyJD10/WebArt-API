namespace WebArt.Application.Features.Users.Commands;

using MediatR;
using System;

public class DeleteUserCommand : IRequest
{
	public Guid Id { get; set; }
}