using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Entities
{
    public class Order :  Entity
    {
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public int OrderTotalPoint { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
