﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Role
{
    public class DeleteRoleRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
