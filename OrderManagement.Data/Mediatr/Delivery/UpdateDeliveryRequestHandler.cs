using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Delivery
{
    public class UpdateDeliveryRequestHandler : IRequestHandler<UpdateDeliveryRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateDeliveryRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateDeliveryRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = DeliveryQueries.UpdateDelivery;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@DeliveryStatus", request.DeliveryStatus);
            command.Parameters.AddWithValue("@Address", request.Address);
            command.Parameters.AddWithValue("@DeliveryDate", request.DeliveryDate);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
