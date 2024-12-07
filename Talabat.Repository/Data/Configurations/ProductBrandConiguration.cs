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
    internal class ProductBrandConiguration :BaseConfiuration<ProductBrand>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100).IsRequired().HasColumnType(SQlSyntax.Varchar);
        }
    }
}
