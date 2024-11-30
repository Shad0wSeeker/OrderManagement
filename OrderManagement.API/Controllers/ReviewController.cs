using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Reviews;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var result = await _mediator.Send(new GetReviewsByProductIdRequest { ProductId = productId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetReviewsByProductId), new { productId = request.ProductId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Review ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _mediator.Send(new DeleteReviewRequest { Id = id });

            if (result == 0)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
