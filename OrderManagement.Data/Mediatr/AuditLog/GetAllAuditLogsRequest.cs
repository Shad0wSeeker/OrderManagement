﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.AuditLog
{
    public class GetAllAuditLogsRequest : IRequest<List<Entities.AuditLog>>
    {
    }
}