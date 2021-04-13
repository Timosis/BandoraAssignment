using Bandora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Entities
{
    public class Equipment : Entity
    {
        public string Name { get; set; }
        public EquiptmentType Type { get; set; }
    }
}
