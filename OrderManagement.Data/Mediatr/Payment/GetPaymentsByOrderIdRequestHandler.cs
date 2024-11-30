using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Payment
{
    public class GetPaymentsByOrderIdRequestHandler : IRequestHandler<GetPaymentsByOrderIdRequest, List<Entities.Payment>>
    {
        private readonly SQLiteConnection _connection;

        public GetPaymentsByOrderIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Payment>> Handle(GetPaymentsByOrderIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = PaymentQueries.GetPaymentsByOrderId;
            query = query.Replace("@OrderId", request.OrderId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var payments = new List<Entities.Payment>();

            while (await reader.ReadAsync(cancellationToken))
            {
                payments.Add(new Entities.Payment
                {
                    Id = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    PaymentStatus = reader.GetString(2),
                    PaymentMethod = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                });
            }

            await _connection.CloseAsync();
            return payments;
        }
    }
}
