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
    public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreatePaymentRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = PaymentQueries.CreatePayment;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@OrderId", request.OrderId);
            command.Parameters.AddWithValue("@PaymentStatus", request.PaymentStatus);
            command.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
