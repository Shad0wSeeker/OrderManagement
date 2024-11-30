using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.OrderItems
{
    public class CreateOrderItemRequestHandler : IRequestHandler<CreateOrderItemRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateOrderItemRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateOrderItemRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderItemQueries.CreateOrderItem;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@OrderId", request.OrderId);
            command.Parameters.AddWithValue("@ProductId", request.ProductId);
            command.Parameters.AddWithValue("@Quantity", request.Quantity);
            command.Parameters.AddWithValue("@Price", request.Price);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
