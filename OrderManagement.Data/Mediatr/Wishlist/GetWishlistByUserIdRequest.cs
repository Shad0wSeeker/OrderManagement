﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Wishlist
{
    public class GetWishlistByUserIdRequest : IRequest<Entities.Wishlist>
    {
        public int UserId { get; set; }
    }
}