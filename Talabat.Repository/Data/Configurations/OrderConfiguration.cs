using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.SubTotal)
                   .HasColumnType("decimal(18,2)");

            builder.Property(O => O.Status)
                   .HasConversion(OStatus => OStatus.ToString(),
                                  OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
