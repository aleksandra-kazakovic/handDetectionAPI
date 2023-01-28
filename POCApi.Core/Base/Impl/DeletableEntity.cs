using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Base.Interfaces;

namespace POCApi.Core.Base.Impl
{
    public abstract class DeleteableEntity<TKey> : EntityBase<TKey>, IDeletableEntity<TKey>
    {
        public bool IsDeleted { get; set; }
    }

}
