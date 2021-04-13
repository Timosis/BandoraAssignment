using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandora.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderTotalPoint { get; set; }
        public int CustomerId { get; set; }
        public List<OrderDetailVM> OrderDetails { get; set; }
    }
}
