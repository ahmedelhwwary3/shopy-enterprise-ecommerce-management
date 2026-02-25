using Enterprise_E_Commerce_Management_System.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(prd=>prd.Variants)
                .WithOne(var=>var.Product)
                .HasForeignKey(var=> var.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(prd => prd.Category)
              .WithMany(ct => ct.Products)
              .HasForeignKey(prd => prd.CategoryId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(prd => new {prd.CategoryId,prd.Name})
              .IsUnique();

            builder.HasIndex(prd => prd.ImageName)
              .IsUnique();

            builder.HasQueryFilter(prd => !prd.IsDeleted);

           

             
        }
    }
}
