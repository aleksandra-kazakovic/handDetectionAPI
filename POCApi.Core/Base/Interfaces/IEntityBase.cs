using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCApi.Core.Base.Interfaces
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
}
