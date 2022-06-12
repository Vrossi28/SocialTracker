using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalIntegration.Twitter.Responses
{
    public class DefaultResponse<T>
    {
        public object Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
