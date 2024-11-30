using MediatR;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Role
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, Entities.Role>
    {
        private readonly SQLiteConnection _connection;

        public GetRoleByIdHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Role> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.GetRoleById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Role? role = null;
            if (await reader.ReadAsync(cancellationToken))
            {
                role = new Entities.Role
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }

            await _connection.CloseAsync();

            return role;
        }
    }
}
