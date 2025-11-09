using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebArt.Application.Features.Orders.Commands;
using WebArt.Application.Features.Orders.Queries;
namespace WebArt.Api.Controllers;

[ApiController]
    [Route("api/[controller]")] 
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // IMPLEMENTACIÓN DE ENDPOINTS CRUD 

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery(); 
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
        
        // GET: api/orders
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery { OrderId = id }; 
            var order = await _mediator.Send(query);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            
            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, command);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
        {
            
            // Opcional, pero recomendado:
             if (id != command.OrderId) 
            {
             return BadRequest("El ID de la ruta no coincide con el del cuerpo");
            }

            await _mediator.Send(command);
            return NoContent(); 
        }

        // DELETE: api/orders
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            //'DeleteOrderCommand'
            var command = new DeleteOrderCommand { OrderId = id };
            await _mediator.Send(command);
        
            return NoContent();
        }
    }
