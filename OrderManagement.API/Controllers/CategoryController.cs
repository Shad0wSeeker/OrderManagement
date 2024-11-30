using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Category;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var request = new GetAllCategoriesWithProductsRequest();
            var result = await _mediator.Send(request);

            if (result == null || result.Count == 0)
            {
                return NotFound("Categories not found.");
            }

            return Ok(result);
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var request = new GetCategoryByIdRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            return Ok(result);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }

            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetCategoryById), new { id = result }, null);
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }

            if (id != request.Id)
            {
                return BadRequest("Category ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Category with id {id} not found.");
            }

            return NoContent(); // Success, no content to return
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var request = new DeleteCategoryRequest { Id = id };
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return NotFound($"Category with id {id} not found.");
            }

            return NoContent(); // Success, no content to return
        }
    }
}
