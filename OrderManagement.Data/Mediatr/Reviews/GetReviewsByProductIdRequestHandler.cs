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
    public class GetReviewsByProductIdRequestHandler : IRequestHandler<GetReviewsByProductIdRequest, List<Entities.Review>>
    {
        private readonly SQLiteConnection _connection;

        public GetReviewsByProductIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Review>> Handle(GetReviewsByProductIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.GetReviewsByProductId;
            query = query.Replace("@ProductId", request.ProductId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var reviews = new List<Entities.Review>();

            while (await reader.ReadAsync(cancellationToken))
            {
                reviews.Add(new Entities.Review
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    ProductId = reader.GetInt32(2),
                    Rating = reader.GetInt32(3),
                    Comment = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5)
                });
            }

            await _connection.CloseAsync();
            return reviews;
        }
    }
}
