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
    public class UpdatePaymentRequestHandler : IRequestHandler<UpdatePaymentRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdatePaymentRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = PaymentQueries.UpdatePayment;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@PaymentStatus", request.PaymentStatus);
            command.Parameters.AddWithValue("@PaymentMethod", request.PaymentMethod);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
