using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class OrderItemQueries
    {
        public static string GetOrderItemsByOrderId = @"
            SELECT * FROM OrderItems WHERE OrderId = @OrderId;
        ";

        public static string CreateOrderItem = @"
            INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) 
            VALUES (@OrderId, @ProductId, @Quantity, @Price);
        ";

        public static string UpdateOrderItem = @"
            UPDATE OrderItems 
            SET Quantity = @Quantity, Price = @Price 
            WHERE Id = @Id;
        ";

        public static string DeleteOrderItem = @"
            DELETE FROM OrderItems WHERE Id = @Id;
        ";
    }
}
