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
    public class DeleteWishlistItemRequestHandler : IRequestHandler<DeleteWishlistItemRequest, int>
    {
        private readonly SQLiteConnection _connection;

        public DeleteWishlistItemRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(DeleteWishlistItemRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WishlistQueries.DeleteWishlistItem;

            using var command = new SQLiteCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", request.Id);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
            return result;
        }
    }
}
