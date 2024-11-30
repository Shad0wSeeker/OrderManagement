using MediatR;
using OrderManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Products
{
    public class GetProductByIdRequest : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
