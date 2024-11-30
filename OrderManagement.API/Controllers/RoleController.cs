using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Mediatr.Role;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Получение роли по ID
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var request = new GetRoleByIdRequest { Id = id };
            var role = await _mediator.Send(request);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // Получение всех ролей
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            var request = new GetAllRolesRequest();
            var roles = await _mediator.Send(request);
            return Ok(roles);
        }

        // Создание новой роли
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> CreateRole([FromBody] CreateRoleRequest request)
        {
            var roleId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetRoleById), new { id = roleId }, roleId);
        }

        // Обновление роли
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> UpdateRole(int id, [FromBody] UpdateRoleRequest request)
        {
            request.Id = id;
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Удаление роли
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> DeleteRole(int id)
        {
            var request = new DeleteRoleRequest { Id = id };
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
