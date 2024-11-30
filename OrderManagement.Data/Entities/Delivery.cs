using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Entities
{
    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string DeliveryStatus { get; set; }
        public string Address { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

}
