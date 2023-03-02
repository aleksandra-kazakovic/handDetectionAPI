using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCApi.RestAPI.Requests
{
    public class ExamineCompartmentPickingRequest
    {
        public int portId { get; set; }
        public int compartmentId { get; set; }
    }
}
