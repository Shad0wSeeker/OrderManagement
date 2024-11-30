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
    public class GetPaymentByIdRequestHandler : IRequestHandler<GetPaymentByIdRequest, Entities.Payment>
    {
        private readonly SQLiteConnection _connection;

        public GetPaymentByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Payment> Handle(GetPaymentByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = PaymentQueries.GetPaymentById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Payment? payment = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                payment = new Entities.Payment
                {
                    Id = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    PaymentStatus = reader.GetString(2),
                    PaymentMethod = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4)
                };
            }

            await _connection.CloseAsync();
            return payment;
        }
    }
}
