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
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateUserRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {            
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.UpdateUser;

            using var command = new SQLiteCommand(query, _connection);

            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Username", request.Username);
            command.Parameters.AddWithValue("@Password", request.PasswordHash);
            command.Parameters.AddWithValue("@RoleId", request.RoleId);
            command.Parameters.AddWithValue("@LastLoginAt", request.LastLoginAt ?? (object)DBNull.Value);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
