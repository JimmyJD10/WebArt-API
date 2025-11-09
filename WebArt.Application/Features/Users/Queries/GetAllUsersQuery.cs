namespace WebArt.Application.Features.Users.Queries;

using MediatR;
using System.Collections.Generic;
using WebArt.Application.DTOs;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}