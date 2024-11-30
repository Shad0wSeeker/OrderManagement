using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Reviews
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateReviewRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.CreateReview;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@ProductId", request.ProductId);
            command.Parameters.AddWithValue("@Rating", request.Rating);
            command.Parameters.AddWithValue("@Comment", request.Comment);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
