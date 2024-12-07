using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            builder.OwnsOne(o => o.Address);
            builder.HasOne(o => o.DeliveryMethod)
                .WithOne();
            //builder.HasIndex(o => o.DeliveryMethodId);

            builder.Property(O=>O.SubTotal).HasColumnType(SQlSyntax.Decimal);

        }
    }
}
