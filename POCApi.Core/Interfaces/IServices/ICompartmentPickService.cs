using POCApi.Core.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Entities;

namespace POCApi.Core.Interfaces.IServices
{
    public interface ICompartmentPickService : IBaseService<CompartmentPick>
    {
        public Task<CompartmentPick> GetLastPickByPortId(int portId);
        public Task<CompartmentPick> IsTakenFromRightCompartment(CompartmentPick compartmentPick);
    }
}
