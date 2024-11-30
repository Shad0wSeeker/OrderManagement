using System;

namespace OrderManagement.Data.Queries
{
    public static class ProductQueries
    {
        public static readonly string CreateProduct = @"
        INSERT INTO Products (Name, Description, Price, Stock, CategoryId, SupplierId) 
        VALUES (@Name, @Description, @Price, @Stock, @CategoryId, @SupplierId);
        SELECT last_insert_rowid();";

        public static readonly string GetProductById = @"
        SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, 
               p.CategoryId, c.Name AS CategoryName, 
               p.SupplierId, s.Name AS SupplierName
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryId = c.Id
        LEFT JOIN Suppliers s ON p.SupplierId = s.Id
        WHERE p.Id = @Id;";

        public static readonly string GetAllProducts = @"
        SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, 
               p.CategoryId, c.Name AS CategoryName, 
               p.SupplierId, s.Name AS SupplierName
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryId = c.Id
        LEFT JOIN Suppliers s ON p.SupplierId = s.Id;";

        public static readonly string UpdateProduct = @"
        UPDATE Products 
        SET Name = @Name, Description = @Description, Price = @Price, 
            Stock = @Stock, CategoryId = @CategoryId, SupplierId = @SupplierId
        WHERE Id = @Id;";

        public static readonly string DeleteProduct = @"
        DELETE FROM Products 
        WHERE Id = @Id;";
    }
}
