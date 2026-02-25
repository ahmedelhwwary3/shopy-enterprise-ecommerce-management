using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using Enterprise_E_Commerce_Management_System.Models.Variants;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class VariantConfiguration : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            builder.Property(v => v.Price)
                .HasPrecision(ValidationConstants.MoneyPrecision
                , ValidationConstants.MoneyScale);

            builder.Property(v => v.Cost)
                .HasPrecision(ValidationConstants.MoneyPrecision
                , ValidationConstants.MoneyScale);

            builder.HasQueryFilter(v => !v.IsDeleted);

            

            builder.HasOne(v => v.Product)
               .WithMany(prd=>prd.Variants)
               .HasForeignKey(v=>v.ProductId)
               .OnDelete(DeleteBehavior.NoAction); 

            builder.HasMany(v => v.AttributeValues)
               .WithOne(prd => prd.Variant)
               .HasForeignKey(v => v.VariantId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(t => t.HasCheckConstraint(
               "CK_Variant_Price",
                $"[Price] > 0"));

            builder.ToTable(t => t.HasCheckConstraint(
               "CK_Variant_Cost",
                $"[Cost] > 0"));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Variant_StockQuantity",
                $"[StockQuantity] BETWEEN" +
                $" {ValidationConstants.MinQuantity} " +
                $"AND {ValidationConstants.MaxQuantity}")); 

            builder.HasIndex(v => v.SKU)
                .IsUnique();

        }
    }
}
