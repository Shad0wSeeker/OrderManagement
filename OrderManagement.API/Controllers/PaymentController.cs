using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Payment;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _mediator.Send(new GetPaymentByIdRequest { Id = id });

            if (result == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }

            return Ok(result);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetPaymentsByOrderId(int orderId)
        {
            var result = await _mediator.Send(new GetPaymentsByOrderIdRequest { OrderId = orderId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetPaymentById), new { id = result }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Payment ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Payment with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _mediator.Send(new DeletePaymentRequest { Id = id });

            if (result == 0)
            {
                return NotFound($"Payment with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
