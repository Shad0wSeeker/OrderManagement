using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class DeliveryQueries
    {
        public static string GetDeliveryById = @"
            SELECT * FROM Deliveries WHERE Id = @Id;
        ";

        public static string GetDeliveriesByOrderId = @"
            SELECT * FROM Deliveries WHERE OrderId = @OrderId;
        ";

        public static string CreateDelivery = @"
            INSERT INTO Deliveries (OrderId, DeliveryStatus, Address, DeliveryDate) 
            VALUES (@OrderId, @DeliveryStatus, @Address, @DeliveryDate);
        ";

        public static string UpdateDelivery = @"
            UPDATE Deliveries 
            SET DeliveryStatus = @DeliveryStatus, Address = @Address, DeliveryDate = @DeliveryDate 
            WHERE Id = @Id;
        ";

        public static string DeleteDelivery = @"
            DELETE FROM Deliveries WHERE Id = @Id;
        ";
    }
}
