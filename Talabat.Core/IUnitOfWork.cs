using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Repostiries_contract;

namespace Talabat.Core
{
    public interface IUnitOfWork
    {

        public IGenaricRepository<T> Repo<T>() where T : BaseEntity,new();

        public Task<int> Commit();
        public void Dispose();
    }
}
