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
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Entities.User>
    {
        private readonly SQLiteConnection _connection;

        public GetUserByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.User> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.GetUserById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.User? user = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                user = new Entities.User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    RoleId = reader.GetInt32(3),
                    CreatedAt = reader.GetDateTime(4),
                    LastLoginAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                };
            }

            await _connection.CloseAsync();
            return user;
        }
    }
}

