using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class RoleQueries
    {
        public static string GetRoleById = @"
            SELECT * FROM Roles WHERE Id = @Id;
        ";

        public static string GetAllRoles = @"
            SELECT * FROM Roles;
        ";

        public static string CreateRole = @"
            INSERT INTO Roles (Name) 
            VALUES (@Name);
        ";

        public static string UpdateRole = @"
            UPDATE Roles 
            SET Name = @Name 
            WHERE Id = @Id;
        ";

        public static string DeleteRole = @"
            DELETE FROM Roles WHERE Id = @Id;
        ";
    }
}
