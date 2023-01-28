using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POCApi.Core.Generic
{
    public class Collection<T>
    {
        public long TotalCount { get; set; }
        [JsonPropertyName("collection")]
        public List<T> Data { get; set; } = new List<T> { };

        public Collection()
        {
            Data = new List<T>();
        }
    }
}
