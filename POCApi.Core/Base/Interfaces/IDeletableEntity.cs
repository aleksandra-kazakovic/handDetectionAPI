using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Core.Base.Interfaces
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeletableEntity<TKey> : IDeletableEntity, IEntityBase<TKey>
    {
    }
}
