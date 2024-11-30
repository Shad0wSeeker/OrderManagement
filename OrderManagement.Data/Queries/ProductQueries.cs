using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class ProductQueries
    {
        public static readonly string CreateProduct = @"
        INSERT INTO Products (Name, Description, Price, Stock, CategoryId) 
        VALUES (@Name, @Description, @Price, @Stock, @CategoryId);
        SELECT last_insert_rowid();";

        public static readonly string GetProductById = @"
        SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, c.Name AS CategoryName
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryId = c.Id
        WHERE p.Id = @Id;";

        public static readonly string GetAllProducts = @"
        SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, c.Name AS CategoryName
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryId = c.Id;";

        public static readonly string UpdateProduct = @"
        UPDATE Products 
        SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock, CategoryId = @CategoryId
        WHERE Id = @Id;";

        public static readonly string DeleteProduct = @"
        DELETE FROM Products 
        WHERE Id = @Id;";
    }

}
