using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.OrderItems
{
    public class GetOrderItemsByOrderIdRequest : IRequest<List<Entities.OrderItem>>
    {
        public int OrderId { get; set; }
    }

}
