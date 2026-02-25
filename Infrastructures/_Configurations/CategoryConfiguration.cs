using Microsoft.EntityFrameworkCore;
using Enterprise_E_Commerce_Management_System.Models.Categories; 
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasQueryFilter(ctg => !ctg.IsDeleted);

            builder.HasMany(ctg => ctg.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(prd => prd.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ctg => ctg.CategoryAttributes)
               .WithOne(ctgAttr=>ctgAttr.Category)
               .HasForeignKey(prd => prd.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ctg => ctg.Parent)
               .WithMany()
               .HasForeignKey(ctg=>ctg.ParentId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
