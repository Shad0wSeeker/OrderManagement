using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Role
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateRoleHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.CreateRole;

            using var command = new SQLiteCommand(query, _connection);

            command.Parameters.AddWithValue("@Name", request.Name);


            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
