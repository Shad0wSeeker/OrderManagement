using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Order
{
    public class GetOrderByIdRequest : IRequest<Entities.Order>
    {
        public int Id { get; set; }
    }
}
