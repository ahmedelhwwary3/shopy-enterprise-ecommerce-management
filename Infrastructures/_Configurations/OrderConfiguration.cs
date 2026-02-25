using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(ord => ord.OrderStatus)
                .HasConversion<byte>()
                .HasComment("Pending=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5");

            builder.ToTable(ord=>ord.HasTrigger("trg_AfterInsertOrder"));

            builder.HasMany(ord => ord.OrderItems)
                .WithOne(item => item.Order)
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ord => ord.Customer)
                .WithMany(cus=>cus.Orders)
                .HasForeignKey(ord=>ord.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ord => ord.Payments)
                .WithOne(cus => cus.Order)
                .HasForeignKey(ord => ord.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(ord => ord.OrderNumber)
                .IsUnique();

            builder.HasIndex(ord => ord.AccessToken) 
                .IsUnique();

            builder.Property(c => c.Email)
             .HasMaxLength(100)
             .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.PostalCode).HasMaxLength(20);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);

                address.HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
            });


            builder.ToTable(t => t.HasCheckConstraint(
               "CK_Order_TotalAmount",
                $"[TotalAmount] BETWEEN" +
                $" {ValidationConstants.MinPrice} " +
                $"AND {ValidationConstants.MaxPrice}"));
        }
    }
}
