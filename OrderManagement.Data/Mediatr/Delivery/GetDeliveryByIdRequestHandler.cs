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
    public class GetDeliveryByIdRequestHandler : IRequestHandler<GetDeliveryByIdRequest, Entities.Delivery>
    {
        private readonly SQLiteConnection _connection;

        public GetDeliveryByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Delivery> Handle(GetDeliveryByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = DeliveryQueries.GetDeliveryById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Delivery? delivery = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                delivery = new Entities.Delivery
                {
                    Id = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    DeliveryStatus = reader.GetString(2),
                    Address = reader.GetString(3),
                    DeliveryDate = reader.GetDateTime(4)
                };
            }

            await _connection.CloseAsync();
            return delivery;
        }
    }
}
