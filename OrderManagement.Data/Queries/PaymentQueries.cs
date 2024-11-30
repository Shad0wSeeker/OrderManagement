using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class PaymentQueries
    {
        public static string GetPaymentById = @"
            SELECT * FROM Payments WHERE Id = @Id;
        ";

        public static string GetPaymentsByOrderId = @"
            SELECT * FROM Payments WHERE OrderId = @OrderId;
        ";

        public static string CreatePayment = @"
            INSERT INTO Payments (OrderId, PaymentStatus, PaymentMethod, CreatedAt) 
            VALUES (@OrderId, @PaymentStatus, @PaymentMethod, @CreatedAt);
        ";

        public static string UpdatePayment = @"
            UPDATE Payments 
            SET PaymentStatus = @PaymentStatus, PaymentMethod = @PaymentMethod 
            WHERE Id = @Id;
        ";

        public static string DeletePayment = @"
            DELETE FROM Payments WHERE Id = @Id;
        ";
    }
}
