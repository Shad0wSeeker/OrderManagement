using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrderManagement.Data.Mediatr.User
{
    public class GetUserByIdRequest : IRequest<Entities.User>
    {
        public int Id { get; set; }
               
    }
}
