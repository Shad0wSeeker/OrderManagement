using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.Supplier;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var result = await _mediator.Send(new GetSupplierByIdRequest { Id = id });

            if (result == null)
                return NotFound($"Supplier with ID {id} not found.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _mediator.Send(new GetAllSuppliersRequest());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetSupplierById), new { id = result }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierRequest request)
        {
            if (id != request.Id)
                return BadRequest("Supplier ID mismatch.");

            var result = await _mediator.Send(request);

            if (result == 0)
                return NotFound($"Supplier with ID {id} not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _mediator.Send(new DeleteSupplierRequest { Id = id });

            if (result == 0)
                return NotFound($"Supplier with ID {id} not found.");

            return NoContent();
        }
    }
}
