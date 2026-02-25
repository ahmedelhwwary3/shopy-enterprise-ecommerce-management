using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures._Configurations
{
    public class OrderReturnConfiguration : IEntityTypeConfiguration<OrderReturn>
    {
        public void Configure(EntityTypeBuilder<OrderReturn> builder)
        {
            builder.HasOne(r => r.Courier)
                .WithMany()
                .HasForeignKey(r => r.CourierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Order)
                .WithOne()
                .HasForeignKey<OrderReturn>(r => r.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(r => r.Notes)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(r => r.Status)
            .HasConversion<byte>()
            .HasComment(" Requested = 1, PickedUp = 2, Returned = 3, Rejected = 4");
        }
    }
}
