using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class CategoryQueries
    {
        public static string GetCategoryById = @"
            SELECT * FROM Categories WHERE Id = @Id;
        ";

        public static string GetAllCategories = @"
            SELECT * FROM Categories;
        ";

        public static string CreateCategory = @"
            INSERT INTO Categories (Name, ParentCategoryId) 
            VALUES (@Name, @ParentCategoryId);
        ";

        public static string UpdateCategory = @"
            UPDATE Categories 
            SET Name = @Name, ParentCategoryId = @ParentCategoryId 
            WHERE Id = @Id;
        ";

        public static string DeleteCategory = @"
            DELETE FROM Categories WHERE Id = @Id;
        ";
    }
}
