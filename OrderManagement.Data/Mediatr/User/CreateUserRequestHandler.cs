using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.User
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateUserRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.CreateUser;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Username", request.Username);
            command.Parameters.AddWithValue("@Password", request.Password);
            command.Parameters.AddWithValue("@RoleId", request.RoleId);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);



            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
