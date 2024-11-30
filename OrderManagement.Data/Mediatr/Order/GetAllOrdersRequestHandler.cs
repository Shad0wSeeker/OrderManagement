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
    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersRequest, List<Entities.Order>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllOrdersRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Order>> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderQueries.GetAllOrders;

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var orders = new List<Entities.Order>();

            while (await reader.ReadAsync(cancellationToken))
            {
                orders.Add(new Entities.Order
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    OrderStatus = reader.GetString(2),
                    TotalAmount = reader.GetDecimal(3),
                    CreatedAt = reader.GetDateTime(4)
                });
            }

            await _connection.CloseAsync();
            return orders;
        }
    }
}
