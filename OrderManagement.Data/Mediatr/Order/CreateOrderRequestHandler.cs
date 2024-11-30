using MediatR;
using OrderManagement.Data.Queries;
using OrderManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Order
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, int>
    {
        private readonly SQLiteConnection _connection;
        private readonly AuditLogService _auditLogService;

        public CreateOrderRequestHandler(SQLiteConnection connection, AuditLogService auditLogService)
        {
            _connection = connection;
            _auditLogService = auditLogService;
        }

        public async Task<int> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = OrderQueries.CreateOrder;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@OrderStatus", request.OrderStatus);
            command.Parameters.AddWithValue("@TotalAmount", request.TotalAmount);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _auditLogService.LogActionAsync(request.UserId, $"Created Order with TotalAmount {request.TotalAmount}");
            await _connection.CloseAsync();
            return result;
        }
    }
}
