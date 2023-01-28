using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Exceptions.Common;

namespace POCApi.Core.Exceptions
{
    public class DemoException : Exception
    {
        public AppError Error { get; set; }

        public DemoException() { }

        public DemoException(AppError error, params object[] data)
            : base(string.Format(error.ErrorMessage, data))
        {
            Error = error;
        }
    }
}
