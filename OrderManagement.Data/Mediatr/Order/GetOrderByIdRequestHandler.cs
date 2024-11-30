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
    public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, Entities.Order>
    {
        private readonly SQLiteConnection _connection;

        public GetOrderByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Order> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderQueries.GetOrderById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Order? order = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                order = new Entities.Order
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    OrderStatus = reader.GetString(2),
                    TotalAmount = reader.GetDecimal(3),
                    CreatedAt = reader.GetDateTime(4)
                };
            }

            await _connection.CloseAsync();
            return order;
        }
    }
}
