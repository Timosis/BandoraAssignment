using System;
using System.Collections.Generic;
using System.Text;

namespace Bandora.Models
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
    }
}
