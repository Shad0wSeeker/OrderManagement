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
    public class UpdateOrderItemRequestHandler : IRequestHandler<UpdateOrderItemRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateOrderItemRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateOrderItemRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderItemQueries.UpdateOrderItem;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Quantity", request.Quantity);
            command.Parameters.AddWithValue("@Price", request.Price);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
