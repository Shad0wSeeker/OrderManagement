using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class DiscountQueries
    {
        public static string GetDiscountByProductId = @"
            SELECT * FROM Discounts WHERE ProductId = @ProductId;
        ";

        public static string CreateDiscount = @"
            INSERT INTO Discounts (ProductId, DiscountPercentage, StartDate, EndDate) 
            VALUES (@ProductId, @DiscountPercentage, @StartDate, @EndDate);
        ";

        public static string UpdateDiscount = @"
            UPDATE Discounts 
            SET DiscountPercentage = @DiscountPercentage, StartDate = @StartDate, EndDate = @EndDate 
            WHERE Id = @Id;
        ";

        public static string DeleteDiscount = @"
            DELETE FROM Discounts WHERE Id = @Id;
        ";
    }
}
