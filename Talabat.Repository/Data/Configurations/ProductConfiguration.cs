using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(P => P.Description)
                   .IsRequired();

            builder.Property(P => P.PictureUrl)
                   .IsRequired();

            builder.Property(P => P.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(P => P.Category)
                   .WithMany()
                   .HasForeignKey(P => P.CategoryId)
                   .OnDelete(DeleteBehavior.ClientSetNull);


        }
    }
}
