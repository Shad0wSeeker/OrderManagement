using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Order
{
    public class UpdateOrderRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
