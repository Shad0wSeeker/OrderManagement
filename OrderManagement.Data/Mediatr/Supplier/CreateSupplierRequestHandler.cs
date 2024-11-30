using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Supplier
{
    public class CreateSupplierRequestHandler : IRequestHandler<CreateSupplierRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateSupplierRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = SupplierQueries.CreateSupplier;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Name", request.Name);
            command.Parameters.AddWithValue("@ContactInfo", request.ContactInfo);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
