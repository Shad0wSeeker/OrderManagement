using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Products
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateProductRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ProductQueries.UpdateProduct;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Name", request.Name);
            command.Parameters.AddWithValue("@Description", request.Description);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@Stock", request.Stock);
            command.Parameters.AddWithValue("@CategoryId", request.CategoryId);
            command.Parameters.AddWithValue("@SupplierId", request.SupplierId);


            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
