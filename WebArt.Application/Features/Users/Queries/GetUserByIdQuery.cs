namespace WebArt.Application.Features.Users.Queries;

using MediatR;
using System;
using WebArt.Application.DTOs;

public class GetUserByIdQuery : IRequest<UserDto?>
{
	public Guid Id { get; set; }
}