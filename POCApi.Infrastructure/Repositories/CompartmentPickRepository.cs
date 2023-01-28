using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Entities;
using POCApi.Core.Interfaces.IRepositories;
using POCApi.Core.Interfaces.IServices;

namespace POCApi.Infrastructure.Repositories
{
    public class CompartmentPickRepository: Repository<CompartmentPick>, ICompartmentPickRepository
    {
        public CompartmentPickRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
