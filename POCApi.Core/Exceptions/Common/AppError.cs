using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Core.Exceptions.Common
{
    public class AppError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
