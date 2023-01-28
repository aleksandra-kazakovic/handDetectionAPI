using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCApi.Core.Generic
{
    public class Query<T> where T: IFilter
    {
        public T Filter { get; set; }
        public Pagination Pagination { get; set; }
        public Sort Sort { get; set; }
    }
}
