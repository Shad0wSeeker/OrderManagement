using MediatR;
using OrderManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Category
{
    public class GetAllCategoriesWithProductsRequestHandler : IRequestHandler<GetAllCategoriesWithProductsRequest, List<CategoryWithProducts>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllCategoriesWithProductsRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<CategoryWithProducts>> Handle(GetAllCategoriesWithProductsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            // Запрос для получения категорий с вложенными продуктами
            var query = @"
                SELECT 
                    c.Id AS CategoryId,
                    c.Name AS CategoryName,
                    p.Id AS ProductId,
                    p.Name AS ProductName,
                    p.Price AS ProductPrice
                FROM Categories c
                LEFT JOIN Products p ON p.CategoryId = c.Id
                ORDER BY c.Id, p.Id";

            var categories = new List<CategoryWithProducts>();
            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Dictionary<int, CategoryWithProducts> categoryMap = new();

            while (await reader.ReadAsync(cancellationToken))
            {
                var categoryId = reader.GetInt32(0);

                if (!categoryMap.TryGetValue(categoryId, out var category))
                {
                    category = new CategoryWithProducts
                    {
                        Id = categoryId,
                        Name = reader.GetString(1),
                        Products = new List<Product>()
                    };
                    categoryMap[categoryId] = category;
                    categories.Add(category);
                }

                if (!reader.IsDBNull(2)) // Если продукт существует
                {
                    category.Products.Add(new Product
                    {
                        Id = reader.GetInt32(2),
                        Name = reader.GetString(3),
                        Price = reader.GetDecimal(4)
                    });
                }
            }

            await _connection.CloseAsync();

            return categories;
        }
    }
}
