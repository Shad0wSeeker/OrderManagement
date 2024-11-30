using MediatR;
using OrderManagement.Data.Mediatr.Role;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Role
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesRequest, List<Entities.Role>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllRolesHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Role>> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);
            var roles = new List<Entities.Role>();
            using var command = new SQLiteCommand(RoleQueries.GetAllRoles, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                roles.Add(new Entities.Role
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            await _connection.CloseAsync();
            return roles;
        }
    }
}

