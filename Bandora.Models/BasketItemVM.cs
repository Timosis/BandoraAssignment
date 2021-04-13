using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandora.Models
{
    public class BasketItemVM
    {
        public int CustomerId { get; set; }
        public int Day { get; set; }
        public int Price { get; set; }
        public int Point { get; set; }
        public EquipmentVM Equipment { get; set; }
    }
}
