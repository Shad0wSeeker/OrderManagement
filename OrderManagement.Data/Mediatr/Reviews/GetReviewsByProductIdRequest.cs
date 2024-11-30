using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Reviews
{
    public class GetReviewsByProductIdRequest : IRequest<List<Entities.Review>>
    {
        public int ProductId { get; set; }
    }
}
