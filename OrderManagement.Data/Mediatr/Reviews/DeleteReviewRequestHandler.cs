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
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public DeleteReviewRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.DeleteReview;
            query = query.Replace("@Id", request.Id.ToString());

            using var command = new SQLiteCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
