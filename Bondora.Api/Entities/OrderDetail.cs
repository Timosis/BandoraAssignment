using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Entities
{
    public class OrderDetail : Entity
    {
        public int OrderId { get; set; }  
        public int EquipmentId { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Points { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
