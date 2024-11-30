using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Wishlist
{
    public class CreateWishlistItemRequest : IRequest<int>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
