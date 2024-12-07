using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Repostiries_contract;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity, new()
    {
        private readonly StoreDbContext db;

        public GenaricRepository(StoreDbContext db)
        {
            this.db = db;
        }



        public async Task<IEnumerable<T>> GetAll()
        {
            /// that's not recommended 
            ///if (typeof(T) == typeof(Product))
            ///    return (IEnumerable<T>) await db.Set<Product>().Include(p=>p.productBrand).Include(p=>p.productCategory).ToListAsync(); 

            return await db.Set<T>().ToListAsync();
        }



        public async Task<T?> GetByID(int id)
        {
            /// that's not recommended 
            ///var item = typeof(T) == typeof(Product)? await db.Set<Product>()
            ///                                        .Include(p => p.productBrand)
            ///                                        .Include(p => p.productCategory)
            ///                                        .Where(p=>p.ID == id).FirstOrDefaultAsync() as T :
            ///                                        await db.Set<T>().FindAsync(id);
            ///                                        

            return await db.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllSpec(ISpecification<T> spec)
        {
            var items = await ApplySpec(spec).AsNoTracking().ToListAsync();
            return items ?? new List<T>();
        }
        public async Task<T> GetByIDSpec(ISpecification<T> spec)
        {
            var items = await ApplySpec(spec).FirstOrDefaultAsync();
            return items ;
        }

        public async Task<int> CountSpec(ISpecification<T> spec)
        {
            var items = await ApplySpec(spec).CountAsync();
            return items ;
        }

        private IQueryable<T> ApplySpec(ISpecification<T> spec)
        => SpecificationEvaluation<T>.GetQuery(db.Set<T>(), spec);

        public async Task AddAsync(T item)
        {
            await db.Set<T>().AddAsync(item);
        }

        public void Updateasync(T item)
        {
            db.Set<T>().Update(item);
        }

        public void DeleteAsync(T item)
        {
            db.Set<T>().Remove(item);
        }
    }
}
