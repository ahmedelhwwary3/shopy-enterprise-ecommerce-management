using Enterprise_E_Commerce_Management_System.Models.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.Property(cp => cp.DiscountType)
                .HasConversion<byte>()
                .HasComment($"Percentage = 1,\nFixedAmount = 2,\nFreeShipping = 3");

            builder.HasMany(cp => cp.Orders)
                .WithOne(ord => ord.Coupon)
                .HasForeignKey(ord=>ord.CouponId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
