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
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateRoleHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.UpdateRole;

            using var command = new SQLiteCommand(query, _connection);

            command.Parameters.AddWithValue("@Id", request.Id.ToString());
            command.Parameters.AddWithValue("@Name", request.Name);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
