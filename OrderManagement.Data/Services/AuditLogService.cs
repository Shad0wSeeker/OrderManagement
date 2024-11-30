using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Services
{
    public class AuditLogService
    {
        private readonly SQLiteConnection _connection;

        public AuditLogService(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task LogActionAsync(int userId, string action)
        {
            await _connection.OpenAsync();

            using var command = new SQLiteCommand(AuditLogQueries.CreateAuditLog, _connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Action", action);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }
}
