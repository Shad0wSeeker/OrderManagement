using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Wishlist;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishlistByUserId(int userId)
        {
            var result = await _mediator.Send(new GetWishlistByUserIdRequest { UserId = userId });

            if (result == null)
                return NotFound($"No wishlist items found for User ID {userId}.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishlistItem([FromBody] CreateWishlistItemRequest request)
        {
            var result = await _mediator.Send(request);

            if (result > 0)
                return Ok("Wishlist item created successfully.");

            return BadRequest("Failed to create wishlist item.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem(int id)
        {
            var result = await _mediator.Send(new DeleteWishlistItemRequest { Id = id });

            if (result == 0)
                return NotFound($"Wishlist item with ID {id} not found.");

            return Ok("Wishlist item deleted successfully.");
        }
    }
}
