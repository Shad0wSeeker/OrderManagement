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
    public class GetAuditLogByIdRequestHandler : IRequestHandler<GetAuditLogByIdRequest, Entities.AuditLog>
    {
        private readonly SQLiteConnection _connection;

        public GetAuditLogByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.AuditLog> Handle(GetAuditLogByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = AuditLogQueries.GetAuditLogById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.AuditLog auditLog = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                auditLog = new Entities.AuditLog
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Action = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3)
                };
            }

            await _connection.CloseAsync();
            return auditLog;
        }
    }
}
