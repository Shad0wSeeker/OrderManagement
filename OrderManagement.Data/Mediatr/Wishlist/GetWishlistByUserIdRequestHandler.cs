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
    public class GetWishlistByUserIdRequestHandler : IRequestHandler<GetWishlistByUserIdRequest, Entities.Wishlist>
    {
        private readonly SQLiteConnection _connection;

        public GetWishlistByUserIdRequestHandler(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Wishlist> Handle(GetWishlistByUserIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WishlistQueries.GetWishlistByUserId;
            query = query.Replace("@UserId", request.UserId.ToString());

            using var command = new SQLiteCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Wishlist? wishlist = null;
                        
            if(await reader.ReadAsync(cancellationToken))
            {
                wishlist = new Entities.Wishlist()
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    ProductId = reader.GetInt32(2)
                };
            }
            

            await _connection.CloseAsync();
            return wishlist;
        }
    }
}
