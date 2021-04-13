using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandora.Models
{
    public enum ResultType
    {
        UnKnown,
        Success,
        Error
    }


    public class ServiceResult : ServiceResult<bool>
    {

    }


    public class ServiceResult<T>
    {
        public ResultType Type { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}
