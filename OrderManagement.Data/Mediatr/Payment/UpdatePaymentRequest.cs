﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Payment
{
    public class UpdatePaymentRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}