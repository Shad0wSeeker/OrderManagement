using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Reviews
{
    public class DeleteReviewRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
