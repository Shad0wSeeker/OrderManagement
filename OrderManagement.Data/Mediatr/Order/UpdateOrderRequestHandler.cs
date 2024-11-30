using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Order
{
    public class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateOrderRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderQueries.UpdateOrder;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@OrderStatus", request.OrderStatus);
            command.Parameters.AddWithValue("@TotalAmount", request.TotalAmount);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
