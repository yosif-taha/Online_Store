using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Store.Domain.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Persistence.Data.Configurations.Orders
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.Cost).HasColumnType("decimal(18,2)");

            builder.Property(D => D.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(D => D.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
        }
    }
}
