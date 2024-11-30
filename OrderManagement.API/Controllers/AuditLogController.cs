using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Mediatr.AuditLog;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditLogById(int id)
        {
            var result = await _mediator.Send(new GetAuditLogByIdRequest { Id = id });

            if (result == null)
                return NotFound($"Audit log with ID {id} not found.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var result = await _mediator.Send(new GetAllAuditLogsRequest());
            return Ok(result);
        }
    }
}
