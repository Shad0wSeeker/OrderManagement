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
    public class GetOrderItemsByOrderIdRequestHandler : IRequestHandler<GetOrderItemsByOrderIdRequest, List<Entities.OrderItem>>
    {
        private readonly SQLiteConnection _connection;

        public GetOrderItemsByOrderIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.OrderItem>> Handle(GetOrderItemsByOrderIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderItemQueries.GetOrderItemsByOrderId;
            query = query.Replace("@OrderId", request.OrderId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var orderItems = new List<Entities.OrderItem>();

            while (await reader.ReadAsync(cancellationToken))
            {
                orderItems.Add(new Entities.OrderItem
                {
                    Id = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    ProductId = reader.GetInt32(2),
                    Quantity = reader.GetInt32(3),
                    Price = reader.GetDecimal(4)
                });
            }

            await _connection.CloseAsync();
            return orderItems;
        }
    }
}
