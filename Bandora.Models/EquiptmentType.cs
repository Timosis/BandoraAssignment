using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandora.Models
{
    public enum EquiptmentType
    {
        [Description("Heavy")]
        Heavy = 1,

        [Description("Regular")]
        Regular = 2,

        [Description("Specialize")]
        Specialize = 3
    }
}
