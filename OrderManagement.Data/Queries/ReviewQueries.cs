using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class ReviewQueries
    {
        public static string GetReviewsByProductId = @"
            SELECT * FROM Reviews WHERE ProductId = @ProductId;
        ";

        public static string CreateReview = @"
            INSERT INTO Reviews (UserId, ProductId, Rating, Comment, CreatedAt) 
            VALUES (@UserId, @ProductId, @Rating, @Comment, @CreatedAt);
        ";

        public static string UpdateReview = @"
            UPDATE Reviews 
            SET Rating = @Rating, Comment = @Comment 
            WHERE Id = @Id;
        ";

        public static string DeleteReview = @"
            DELETE FROM Reviews WHERE Id = @Id;
        ";
    }
}
