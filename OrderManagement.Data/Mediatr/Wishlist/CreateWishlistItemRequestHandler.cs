using MediatR;
using OrderManagement.Data.Queries;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Wishlist
{
    public class CreateWishlistItemRequestHandler : IRequestHandler<CreateWishlistItemRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public CreateWishlistItemRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(CreateWishlistItemRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WishlistQueries.CreateWishlistItem;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@ProductId", request.ProductId);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
