using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Core.Base.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        long? CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        long? UpdatedBy { get; set; }
    }
    public interface IAuditableEntity<TKey> : IAuditableEntity, IDeletableEntity<TKey>
    {
    }
}
