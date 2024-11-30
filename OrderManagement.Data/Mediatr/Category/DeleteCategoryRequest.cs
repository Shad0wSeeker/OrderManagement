using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Mediatr.Category
{
    public class DeleteCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
}
