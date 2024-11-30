using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class OrderQueries
    { 
        public static string GetOrderById = @"
            SELECT * FROM Orders WHERE Id = @Id;
        ";

        public static string GetAllOrders = @"
            SELECT * FROM Orders;
        ";

        public static string CreateOrder = @"
            INSERT INTO Orders (UserId, OrderStatus, TotalAmount, CreatedAt) 
            VALUES (@UserId, @OrderStatus, @TotalAmount, @CreatedAt);
        ";

        public static string UpdateOrder = @"
            UPDATE Orders 
            SET OrderStatus = @OrderStatus, TotalAmount = @TotalAmount 
            WHERE Id = @Id;
        ";

        public static string DeleteOrder = @"
            DELETE FROM Orders WHERE Id = @Id;
        ";
    }
}
