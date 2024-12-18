﻿using MediatR;
using OrderManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Category
{
    public class GetAllCategoriesWithProductsRequest : IRequest<List<CategoryWithProducts>>
    {
    }
    public class CategoryWithProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
