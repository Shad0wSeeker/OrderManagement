using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.User
{
    public class CreateUserRequest : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
