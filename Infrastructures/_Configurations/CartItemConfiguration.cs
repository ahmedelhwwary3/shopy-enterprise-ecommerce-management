using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enterprise_E_Commerce_Management_System.Models.CartItems;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(i=>i.Cart)
                .WithMany(crt=>crt.CartItems)
                .HasForeignKey(i=>i.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Variant)
                .WithMany()
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_CartItem_Quantity",
                 $"[Quantity] BETWEEN" +
                 $" {ValidationConstants.MinQuantity} " +
                 $"AND {ValidationConstants.MaxQuantity}"));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_CartItem_Quantity",
                 $"[UnitPrice] BETWEEN" +
                 $" {ValidationConstants.MinPrice} " +
                 $"AND {ValidationConstants.MaxPrice}"));
        }
    }
}
