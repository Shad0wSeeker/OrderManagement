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
    public class UpdateDiscountRequestHandler : IRequestHandler<UpdateDiscountRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public UpdateDiscountRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(UpdateDiscountRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = DiscountQueries.UpdateDiscount;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);
            command.Parameters.AddWithValue("@DiscountPercentage", request.DiscountPercentage);
            command.Parameters.AddWithValue("@StartDate", request.StartDate);
            command.Parameters.AddWithValue("@EndDate", request.EndDate);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
