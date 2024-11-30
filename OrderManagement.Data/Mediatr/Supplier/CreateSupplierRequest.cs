using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Supplier
{
    public class CreateSupplierRequest : IRequest<int>
    {
        public string Name { get; set; }
        public string ContactInfo { get; set; }
    }
}
