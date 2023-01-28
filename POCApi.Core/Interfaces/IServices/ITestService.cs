using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCApi.Core.Generic;

namespace POCApi.Core.Interfaces.IServices
{
    public interface ITestService<T>
    {
        public Task<T> Add(T t);
        public Task<T> GetByIdAsync(long id);
        public Task<Collection<T>> GetAll();
        public Task<T> Update(T t);
        public Task<T> Delete(long id);
    }
}
