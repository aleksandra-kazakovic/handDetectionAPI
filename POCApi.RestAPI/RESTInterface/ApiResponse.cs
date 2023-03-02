using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace POCApi.RestAPI.RESTInterface
{
    public class ApiResponse<T> where T: class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public T Data { get; set; }
    }
}
