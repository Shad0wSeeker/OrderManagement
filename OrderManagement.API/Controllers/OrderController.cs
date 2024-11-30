using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Order;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var request = new GetAllOrdersRequest();
            var result = await _mediator.Send(request);

            if (result == null || result.Count == 0)
            {
                return NotFound("No orders found.");
            }

            return Ok(result);
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var request = new GetOrderByIdRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return NotFound($"Order with id {id} not found.");
            }

            return Ok(result);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order data.");
            }

            var orderId = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { id = orderId });
        }

        // PUT: api/order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
        {
            if (request == null || id != request.Id)
            {
                return BadRequest("Invalid order data or ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Order with id {id} not found.");
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var request = new DeleteOrderRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Order with id {id} not found.");
            }

            return NoContent(); // 204 No Content
        }
    }
}
