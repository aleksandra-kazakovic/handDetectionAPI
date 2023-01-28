using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCApi.Core.Generic
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Pagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
