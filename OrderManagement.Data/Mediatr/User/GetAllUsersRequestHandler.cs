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
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, List<Entities.User>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllUsersRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.User>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            // Открываем соединение с базой данных
            await _connection.OpenAsync(cancellationToken);

            var users = new List<Entities.User>();

            using var command = _connection.CreateCommand();
            command.CommandText = UserQueries.GetAllUsers;

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync())
            {
                users.Add(new Entities.User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    RoleId = reader.GetInt32(3),
                    CreatedAt = reader.GetDateTime(4),
                    LastLoginAt = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5)
                });
            }

            await _connection.CloseAsync();
            return users;
        }
    }
}
