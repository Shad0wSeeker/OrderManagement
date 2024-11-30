using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Mediatr.Products;
using OrderManagement.Data.Mediatr.User;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetProductsById(int id)
        {
            var request = new GetProductByIdRequest { Id = id };
            var product = await _mediator.Send(request);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllProducts()
        {
            var request = new GetAllProductsRequest();
            var products = await _mediator.Send(request);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (request.CategoryId <= 0)
            {
                return BadRequest("Invalid CategoryId.");
            }

            var productId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetProductsById), new { id = productId }, productId);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            request.Id = id;  // Присваиваем ID из URL
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteProduct(int id)
        {
            var request = new DeleteProductRequest { Id = id };
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
