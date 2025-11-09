using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebArt.Application.Features.Users.Queries;
using WebArt.Application.Features.Users.Commands;
namespace WebArt.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // esta es la ruta base
public class UsersController : ControllerBase
{

    // Integracion de media MediatR
    private readonly IMediator _mediator;

    // Inyección de Dependencias
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
        {
        }
    }
   
// GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);
        return Ok(users); 
        // Devuelve 200 OK con la lista de usuarios
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var query = new GetUserByIdQuery { UserId = id };
        var user = await _mediator.Send(query);

        if (user == null)
        {
            return NotFound(); 
            // Devuelve 404 Not Found
        }

        return Ok(user); 
        // Devuelve 200 OK con el usuario
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        
        var userId = await _mediator.Send(command);

        // Devuelve 201 Created con la ubicación del nuevo recurso
        return CreatedAtAction(nameof(GetUserById), new { id = userId }, command);
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        // Es importante asegurarse que el ID de la ruta coincida
        // con el ID en el cuerpo del comando.
        if (id != command.UserId)
        {
            return BadRequest(); 
            // Devuelve 400 Bad Request
        }

        await _mediator.Send(command);
        return NoContent(); 
        // Devuelve 204 No Content
    }

    // DELETE: api/users
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var command = new DeleteUserCommand { UserId = id };
        await _mediator.Send(command);

        return NoContent(); 
        // Devuelve 204 No Content
    }
}