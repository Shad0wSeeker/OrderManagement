using MediatR;
using OrderManagement.Data.Entities;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Products
{
    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, Product>
    {
        private readonly SQLiteConnection _connection;

        public GetProductByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ProductQueries.GetProductById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Product? product = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Stock = reader.GetInt32(4),
                    CategoryId = reader.GetInt32(5),
                    CategoryName = reader.IsDBNull(6) ? null : reader.GetString(6),
                    SupplierId = reader.GetInt32(7),
                    SupplierName = reader.IsDBNull(8) ? null : reader.GetString(8),
                };
            }

            await _connection.CloseAsync();
            return product;
        }
    }
}
