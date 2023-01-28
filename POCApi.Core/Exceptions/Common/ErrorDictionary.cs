using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Core.Exceptions.Common
{
    public static class ErrorDictionary
    {
        public static AppError ErrInternalServerError = new AppError { ErrorCode = "ERR_INTERNAL_SERVER_ERROR", ErrorMessage = "Unexpected error occured." };

    }
}
