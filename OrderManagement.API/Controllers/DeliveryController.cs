using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Delivery;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/deliveries
        [HttpGet]
        public async Task<IActionResult> GetDeliveriesByOrderId([FromQuery] int orderId)
        {
            var request = new GetDeliveriesByOrderIdRequest { OrderId = orderId };
            var result = await _mediator.Send(request);

            if (result == null || result.Count == 0)
            {
                return NotFound("No deliveries found for this order.");
            }

            return Ok(result);
        }

        // GET: api/deliveries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            var request = new GetDeliveryByIdRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return NotFound($"Delivery with id {id} not found.");
            }

            return Ok(result);
        }

        // POST: api/deliveries
        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid delivery data.");
            }

            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetDeliveryById), new { id = result }, null);
        }

        // PUT: api/deliveries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDelivery(int id, [FromBody] UpdateDeliveryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid delivery data.");
            }

            if (id != request.Id)
            {
                return BadRequest("Delivery ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Delivery with id {id} not found.");
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/deliveries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var request = new DeleteDeliveryRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Delivery with id {id} not found.");
            }

            return NoContent(); // Success, no content to return
        }
    }
}
