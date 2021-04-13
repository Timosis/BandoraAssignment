using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Entities
{
    public class Customer : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }        
    }
}
