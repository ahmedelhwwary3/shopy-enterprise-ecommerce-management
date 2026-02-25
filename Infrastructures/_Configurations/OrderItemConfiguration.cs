using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(item => item.Order)
                .WithMany(ord => ord.OrderItems)
                .HasForeignKey(item=>item.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(item => item.Variant)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_OrderItem_Quantity",
                 $"[Quantity] BETWEEN" +
                 $" {ValidationConstants.MinQuantity} " +
                 $"AND {ValidationConstants.MaxQuantity}"));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_OrderItem_Price",
                 $"[Price] BETWEEN" +
                 $" {ValidationConstants.MinPrice} " +
                 $"AND {ValidationConstants.MaxPrice}"));
        }

    }
}
