using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Delivery
{
    public class GetDeliveriesByOrderIdRequest : IRequest<List<Entities.Delivery>>
    {
        public int OrderId { get; set; }
    }
}
