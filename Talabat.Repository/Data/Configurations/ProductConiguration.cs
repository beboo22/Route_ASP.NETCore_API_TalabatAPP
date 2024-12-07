using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductConiguration : BaseConfiuration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100).IsRequired().HasColumnType(SQlSyntax.Varchar);
            builder.Property(p => p.price).IsRequired().HasColumnType(SQlSyntax.Decimal);
            builder.Property(p => p.PictureUrl).IsRequired().HasColumnType(SQlSyntax.NVarchar);
            builder.Property(p => p.Description).IsRequired().HasColumnType(SQlSyntax.NVarchar);
            builder.HasOne(p => p.productBrand).WithMany().HasForeignKey(p=>p.BrandId);
            builder.HasOne(p=>p.productCategory).WithMany().HasForeignKey(p=>p.CategoryId);
            builder.Property(p => p.BrandId).HasColumnName("CategoryId").IsRequired(false);
            builder.Property(p => p.CategoryId).HasColumnName("BrandId").IsRequired(false);
            
        }

    }
}
