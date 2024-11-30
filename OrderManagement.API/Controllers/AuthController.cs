using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Services;
using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            // Проверяем пользователя из базы данных
            var user = GetUserByUsernameAndPassword(login.Username, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // Получаем роль из базы данных
            var role = GetRoleById(user.RoleId);
            if (role == null)
                return Unauthorized("User role not found");

            var token = GenerateJwtToken(user.Username, role.Name);
            return Ok(new { Token = token });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity.Name; // Извлекаем имя пользователя из токена

            var user = GetUserByUsername(username);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            return Ok(new { Username = user.Username });
        }

        // Метод для получения пользователя по имени
        private User GetUserByUsername(string username)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Users 
                WHERE Username = @Username;
            ";
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    RoleId = reader.GetInt32(3),
                    CreatedAt = reader.GetDateTime(4),
                    LastLoginAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                };
            }

            return null;
        }

        private User GetUserByUsernameAndPassword(string username, string password)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Users 
                WHERE Username = @Username AND Password = @Password;
            ";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    RoleId = reader.GetInt32(3),
                    CreatedAt = reader.GetDateTime(4),
                    LastLoginAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                };
            }

            return null;
        }

        private Role GetRoleById(int roleId)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Roles WHERE Id = @RoleId;
            ";
            command.Parameters.AddWithValue("@RoleId", roleId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Role
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }

            return null;
        }

        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
