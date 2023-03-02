using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POCApi.Core.DomainServices;
using POCApi.Core.Entities;
using POCApi.Core.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Microsoft.Office.Interop.Excel;
using POCApi.RestAPI.Requests;
using POCApi.RestAPI.RESTInterface;

namespace POCApi.RestAPI.Controllers
{
    [ApiController]
    [Route("api/v1/compartment")]
    public class CompartmentPickController : RestController
    {
        private readonly CompartmentPickService _compartmentPickService;

        public CompartmentPickController(CompartmentPickService compartmentPickService, IMapper mapper)
            : base(mapper)
        {
            _compartmentPickService = compartmentPickService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CompartmentPick>>> GetById(long id)
        {
            var compartmentPick = await _compartmentPickService.GetByIdAsync(id);
            return (compartmentPick==null) ?  NotFound("There is no compartment pick with that id") :  Success(_mapper.Map<CompartmentPick>(compartmentPick));
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<ApiResponse<Collection<CompartmentPick>>>> GetAll()
        {
            var compartmentCollection = await _compartmentPickService.GetAll();
            return Collection<CompartmentPick, CompartmentPick>(compartmentCollection);
        }
        [HttpGet("getLast{portId}")]
        public async Task<ActionResult<ApiResponse<CompartmentPick>>> GetLastByPortId(int portId)
        {
            var compartmentPick = await _compartmentPickService.GetLastPickByPortId(portId);
            return (compartmentPick==null) ?  NotFound("There is no picks for that port id") :  Success(_mapper.Map<CompartmentPick>(compartmentPick));
             
        }
        [HttpGet("isTakenFromExpectedCompartment/{portId}/{expectedCompartmentId}")]
        public async Task<ActionResult<ApiResponse<string>>> IsTakenFromExpectedCompartment(int portId, int expectedCompartmentId)
        {
            var compartmentPick = await _compartmentPickService.IsTakenFromExpectedCompartment(portId, expectedCompartmentId);
            return (compartmentPick==null) ?  Success("Not taken from the expected compartment") : Success("It was taken from expected compartment");
        }
        
    }
}
