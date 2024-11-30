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
    public class GetDeliveriesByOrderIdRequestHandler : IRequestHandler<GetDeliveriesByOrderIdRequest, List<Entities.Delivery>>
    {
        private readonly SQLiteConnection _connection;

        public GetDeliveriesByOrderIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Delivery>> Handle(GetDeliveriesByOrderIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = DeliveryQueries.GetDeliveriesByOrderId;
            query = query.Replace("@OrderId", request.OrderId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var deliveries = new List<Entities.Delivery>();

            while (await reader.ReadAsync(cancellationToken))
            {
                deliveries.Add(new Entities.Delivery
                {
                    Id = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    DeliveryStatus = reader.GetString(2),
                    Address = reader.GetString(3),
                    DeliveryDate = reader.GetDateTime(4)
                });
            }

            await _connection.CloseAsync();
            return deliveries;
        }
    }
}
