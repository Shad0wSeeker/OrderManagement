using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.AuditLog
{
    public class GetAllAuditLogsRequestHandler : IRequestHandler<GetAllAuditLogsRequest, List<Entities.AuditLog>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllAuditLogsRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.AuditLog>> Handle(GetAllAuditLogsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = AuditLogQueries.GetAllAuditLogs;

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var auditLogs = new List<Entities.AuditLog>();

            while (await reader.ReadAsync(cancellationToken))
            {
                auditLogs.Add(new Entities.AuditLog
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Action = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3)
                });
            }

            await _connection.CloseAsync();
            return auditLogs;
        }
    }
}
