﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Supplier
{
    public class GetAllSuppliersRequest : IRequest<List<Entities.Supplier>>
    {
    }
}