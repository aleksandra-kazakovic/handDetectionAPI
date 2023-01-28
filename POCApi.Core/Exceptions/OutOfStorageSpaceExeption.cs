using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Exceptions.Common;

namespace POCApi.Core.Exceptions
{
    public class OutOfStorageSpaceExeption : DemoException
    {
        public OutOfStorageSpaceExeption() { }

        public OutOfStorageSpaceExeption(AppError appError, params object[] parameters)
            : base(appError, parameters)
        { }
    }
}
