using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Products
{
    public class DeleteProductRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
