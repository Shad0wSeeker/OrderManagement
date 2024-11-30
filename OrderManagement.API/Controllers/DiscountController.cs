using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Discount;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/discounts?productId=5
        [HttpGet]
        public async Task<IActionResult> GetDiscountByProductId([FromQuery] int productId)
        {
            var request = new GetDiscountByProductIdRequest { ProductId = productId };
            var result = await _mediator.Send(request);

            if (result == null || result.Count == 0)
            {
                return NotFound("No discounts found for this product.");
            }

            return Ok(result);
        }

        // POST: api/discounts
        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid discount data.");
            }

            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetDiscountByProductId), new { productId = request.ProductId }, null);
        }

        // PUT: api/discounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] UpdateDiscountRequest request)
        {
            if (request == null || id != request.Id)
            {
                return BadRequest("Invalid discount data.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Discount with id {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/discounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var request = new DeleteDiscountRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Discount with id {id} not found.");
            }

            return NoContent();
        }
    }
}
