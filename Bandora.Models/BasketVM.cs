using System;
using System.Collections.Generic;
using System.Text;

namespace Bandora.Models
{
    public class BasketVM
    {        
        public int CustomerId { get; set; }
        public List<BasketItemVM> BasketItems { get; set; }
    }
}
