using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class WishlistQueries
    {
        public static string GetWishlistByUserId = @"
            SELECT * FROM Wishlists WHERE UserId = @UserId;
        ";

        public static string CreateWishlistItem = @"
            INSERT INTO Wishlists (UserId, ProductId) 
            VALUES (@UserId, @ProductId);
        ";

        public static string DeleteWishlistItem = @"
            DELETE FROM Wishlists WHERE Id = @Id;
        ";
    }
}
