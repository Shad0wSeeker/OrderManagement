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
    public class GetAllSuppliersRequestHandler : IRequestHandler<GetAllSuppliersRequest, List<Entities.Supplier>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllSuppliersRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Supplier>> Handle(GetAllSuppliersRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = SupplierQueries.GetAllSuppliers;

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var suppliers = new List<Entities.Supplier>();

            while (await reader.ReadAsync(cancellationToken))
            {
                suppliers.Add(new Entities.Supplier
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ContactInfo = reader.GetString(2)
                });
            }

            await _connection.CloseAsync();
            return suppliers;
        }
    }
}
