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
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<Product>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllProductsRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Product>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var products = new List<Product>();

            using var command = _connection.CreateCommand();
            command.CommandText = ProductQueries.GetAllProducts;

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Stock = reader.GetInt32(4),
                    CategoryId = reader.GetInt32(5),
                    CategoryName = reader.IsDBNull(6) ? null : reader.GetString(6),
                });
            }

            await _connection.CloseAsync();
            return products;
        }
    }
}
