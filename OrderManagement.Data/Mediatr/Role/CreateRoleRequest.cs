using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Role
{
    public class CreateRoleRequest : IRequest<int>
    {
        public string Name { get; set; }
    }
}
