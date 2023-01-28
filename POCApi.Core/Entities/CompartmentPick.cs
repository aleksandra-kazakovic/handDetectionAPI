using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Base.Impl;

namespace POCApi.Core.Entities
{
    public class CompartmentPick : EntityBase<long>
    {
        public int PortId { get; set; }
        public int BinType { get; set; }
        public int  CompartmentId{ get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}
