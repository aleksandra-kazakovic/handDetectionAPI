using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Entities;
using POCApi.Core.Generic;
using POCApi.Core.Interfaces;
using POCApi.Core.Interfaces.IRepositories;
using POCApi.Core.Interfaces.IServices;

namespace POCApi.Core.DomainServices
{
    public class CompartmentPickService : ICompartmentPickService
    {
        private readonly ICompartmentPickRepository _compartmentPickRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompartmentPickService(ICompartmentPickRepository compartmentPickRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _compartmentPickRepository = compartmentPickRepository;
        }
        public async Task<CompartmentPick> Add(CompartmentPick compartmentPick)
        {
            _compartmentPickRepository.Add(compartmentPick);
            await _unitOfWork.CommitAsync();
            return compartmentPick;

        }
        public async Task<CompartmentPick> GetByIdAsync(long id)
        {
            var compartmentPick = await _compartmentPickRepository.Get(x => x.Id == id);
            return compartmentPick;
        }
        public async Task<CompartmentPick> Delete(long id)
        {
            var existingPick = await _compartmentPickRepository.Get(id);
            if (existingPick != null)
            {
                _compartmentPickRepository.Delete(existingPick);
                await _unitOfWork.CommitAsync();
            }
            // TODO: error handling
            return existingPick;
        }
        public async Task<CompartmentPick> Update(CompartmentPick compartmentPick)
        {
            _compartmentPickRepository.Update(compartmentPick);
            await _unitOfWork.CommitAsync();
            return compartmentPick;
        }

        public async Task<Collection<CompartmentPick>> GetAll()
        {
            // TODO: implement search
            var result = await _compartmentPickRepository.List();
            return new Collection<CompartmentPick>()
            {
                Data = result,
                TotalCount = result.Count
            };
        }

        public async Task<CompartmentPick> GetLastPickByPortId(int portId)
        {
            var picks = await _compartmentPickRepository.List(x => x.PortId == portId);
            return (picks?.Count > 0) ? picks.Last() : null;
        }

        public async Task<CompartmentPick> IsTakenFromRightCompartment(CompartmentPick compartmentPick)
        {
            var pick = await _compartmentPickRepository.Get(x => x.PortId == compartmentPick.PortId && x.CompartmentId == compartmentPick.CompartmentId && x.CreationTimestamp.AddSeconds(10) >= DateTime.Now);
            return pick;
        }
    }
}
