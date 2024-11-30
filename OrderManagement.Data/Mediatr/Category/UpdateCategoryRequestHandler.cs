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
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateCategoryRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CategoryQueries.UpdateCategory;

            using var command = new SQLiteCommand(query, _connection);

            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@Name", request.Name);
            command.Parameters.AddWithValue("@ParentCategoryId", request.ParentCategoryId ?? (object)DBNull.Value);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
