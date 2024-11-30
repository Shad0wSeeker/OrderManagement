using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.OrderItems;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/orderitems?orderId=5
        [HttpGet]
        public async Task<IActionResult> GetOrderItemsByOrderId([FromQuery] int orderId)
        {
            var request = new GetOrderItemsByOrderIdRequest { OrderId = orderId };
            var result = await _mediator.Send(request);

            if (result == null || result.Count == 0)
            {
                return NotFound("No order items found for this order.");
            }

            return Ok(result);
        }

        // POST: api/orderitems
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order item data.");
            }

            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetOrderItemsByOrderId), new { orderId = request.OrderId }, null);
        }

        // PUT: api/orderitems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] UpdateOrderItemRequest request)
        {
            if (request == null || id != request.Id)
            {
                return BadRequest("Invalid order item data.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Order item with id {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/orderitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var request = new DeleteOrderItemRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Order item with id {id} not found.");
            }

            return NoContent();
        }
    }
}
