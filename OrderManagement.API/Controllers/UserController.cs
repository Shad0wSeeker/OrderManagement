using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Mediatr.User;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Получение пользователя по ID
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var request = new GetUserByIdRequest { Id = id };
            var user = await _mediator.Send(request);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Получение всех пользователей
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var request = new GetAllUsersRequest();
            var users = await _mediator.Send(request);
            return Ok(users);
        }

        // Создание нового пользователя
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserRequest request)
        {
            var userId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }

        // Обновление пользователя
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            request.Id = id;  // Присваиваем ID из URL
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Удаление пользователя
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            var request = new DeleteUserRequest { Id = id };
            var rowsAffected = await _mediator.Send(request);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
