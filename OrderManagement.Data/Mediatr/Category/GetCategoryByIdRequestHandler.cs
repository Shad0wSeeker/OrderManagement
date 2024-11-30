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
    public class GetCategoryByIdRequestHandler : IRequestHandler<GetCategoryByIdRequest, Entities.Category>
    {
        private readonly SQLiteConnection _connection;

        public GetCategoryByIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Category> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CategoryQueries.GetCategoryById;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Category category = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                category = new Entities.Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ParentCategoryId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                };
            }

            await _connection.CloseAsync();
            return category;
        }
    }
}
