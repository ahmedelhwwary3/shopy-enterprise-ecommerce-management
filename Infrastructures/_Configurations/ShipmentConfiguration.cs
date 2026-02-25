using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
             builder.Property(shp => shp.ShipmentStatus)
                .HasConversion<byte>()
                .HasComment("AssignedForDelivery = 1, Delivered = 2, DeliveryFailed = 3");

             builder.HasOne(shp=>shp.Order)
                .WithMany(o=>o.Shipments)
                .HasForeignKey(shp=>shp.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(shp => shp.OrderStatus)
                .HasConversion<byte>()
                .HasComment("Pending=1, Paid=2,   InDelivery=3,   Cancelled=4");


        }
    }
}
