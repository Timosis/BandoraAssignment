using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandora.Models
{
    public class OrderDetailVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int EquipmentId { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Points { get; set; }

        public EquipmentVM Equipment { get; set; }
    }
}
