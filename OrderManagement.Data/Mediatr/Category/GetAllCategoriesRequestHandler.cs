using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Category
{
    public class GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, List<Entities.Category>>
    {
        private readonly SQLiteConnection _connection;

        public GetAllCategoriesRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Category>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CategoryQueries.GetAllCategories;

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var categories = new List<Entities.Category>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var category = new Entities.Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ParentCategoryId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                };

                categories.Add(category);
            }

            await _connection.CloseAsync();

            return categories;
        }
    }
}
