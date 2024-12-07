using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Talabat.Core.Repostiries_contract
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        public Task<T> GetByID(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetByIDSpec(ISpecification<T> spec);
        public Task<IEnumerable<T>> GetAllSpec(ISpecification<T> spec);
        public Task<int> CountSpec(ISpecification<T> spec);

        public Task AddAsync(T item);
        public void Updateasync(T item);
        public void DeleteAsync(T item);

    }
}
