using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Discount
{
    public class GetDiscountByProductIdRequestHandler : IRequestHandler<GetDiscountByProductIdRequest, List<Entities.Discount>>
    {
        private readonly SQLiteConnection _connection;

        public GetDiscountByProductIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Discount>> Handle(GetDiscountByProductIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = DiscountQueries.GetDiscountByProductId;
            query = query.Replace("@ProductId", request.ProductId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var discounts = new List<Entities.Discount>();

            while (await reader.ReadAsync(cancellationToken))
            {
                discounts.Add(new Entities.Discount
                {
                    Id = reader.GetInt32(0),
                    ProductId = reader.GetInt32(1),
                    DiscountPercentage = reader.GetDecimal(2),
                    StartDate = reader.GetDateTime(3),
                    EndDate = reader.GetDateTime(4)
                });
            }

            await _connection.CloseAsync();
            return discounts;
        }
    }
}
