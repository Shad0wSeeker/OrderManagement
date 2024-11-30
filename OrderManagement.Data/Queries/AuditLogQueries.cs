using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class AuditLogQueries
    {
        public static string GetAuditLogById = @"
            SELECT * FROM AuditLogs WHERE Id = @Id;
        ";

        public static string GetAllAuditLogs = @"
            SELECT * FROM AuditLogs;
        ";

        public static string CreateAuditLog = @"
            INSERT INTO AuditLogs (UserId, Action, CreatedAt) 
            VALUES (@UserId, @Action, @CreatedAt);
        ";

        public static string UpdateAuditLog = @"
            UPDATE AuditLogs 
            SET Action = @Action, CreatedAt = @CreatedAt 
            WHERE Id = @Id;
        ";

        public static string DeleteAuditLog = @"
            DELETE FROM AuditLogs WHERE Id = @Id;
        ";
    }
}
