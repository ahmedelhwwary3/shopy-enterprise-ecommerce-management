using Enterprise_E_Commerce_Management_System.Models.Payments;
using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(pay => pay.PaymentMethod)
                .HasConversion<byte>()
                .HasComment("CashOnDelivery = 1,\nCreditCard = 2,\nWallet = 3");

            builder.Property(pay => pay.PaymentStatus)
                .HasConversion<byte>()
                .HasComment("\nPending = 1,\nPaid = 2,\nFailed = 3,\nRefunded = 4"); 

            builder.HasOne(pay => pay.Customer)
                .WithMany(cus => cus.Payments)
                .HasForeignKey(cus=>cus.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable(t => t.HasCheckConstraint(
              "CK_Payment_Amount",
               $"[Amount] BETWEEN" +
               $" {ValidationConstants.MinPrice} " +
               $"AND {ValidationConstants.MaxPrice}"));
        }
    }
}
