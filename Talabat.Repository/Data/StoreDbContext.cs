using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.Repository.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductBrand> Brands { get; set; }
        DbSet<ProductCategory> Category { get; set; }
    }
}
