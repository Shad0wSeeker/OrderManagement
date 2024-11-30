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
    public class GetSupplierByIdRequestHandler : IRequestHandler<GetSupplierByIdRequest, Entities.Supplier>
    {
        private readonly SQLiteConnection _connection;

        public GetSupplierByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Supplier> Handle(GetSupplierByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = SupplierQueries.GetSupplierById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Supplier? supplier = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                supplier = new Entities.Supplier
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ContactInfo = reader.GetString(2)
                };
            }

            await _connection.CloseAsync();
            return supplier;
        }
    }
}
