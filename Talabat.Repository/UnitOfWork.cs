using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Repostiries_contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repository;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable DictRepo;
        public UnitOfWork(StoreDbContext db)
        {
            _db = db;
            DictRepo = new Hashtable();
        }

        public StoreDbContext _db { get; }

        public async Task<int> Commit()
        {
            try
            {

            return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IGenaricRepository<T> Repo<T>() where T : BaseEntity, new()
        {
            var Key = typeof(T).Name;
            if (!DictRepo.ContainsKey(Key))
            {
                var repo = new GenaricRepository<T>(_db);

                DictRepo.Add(Key, repo);
            }

            return (IGenaricRepository<T>)DictRepo[Key];

        }
    }
}
