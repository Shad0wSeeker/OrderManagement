using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class UserQueries
    {        
        public static string GetUserById = @"
            SELECT * FROM Users WHERE Id = @Id;
        ";

        public static string GetAllUsers = @"
            SELECT * FROM Users;
        ";

        public static string CreateUser = @"
            INSERT INTO Users (Username, Password, RoleId, CreatedAt) 
            VALUES (@Username, @Password, @RoleId, @CreatedAt);
        ";

        public static string UpdateUser = @"
            UPDATE Users 
            SET Username = @Username, Password = @Password, RoleId = @RoleId, LastLoginAt = @LastLoginAt
            WHERE Id = @Id;
        ";

        public static string DeleteUser = @"
            DELETE FROM Users WHERE Id = @Id;
        ";
    }
    
}
