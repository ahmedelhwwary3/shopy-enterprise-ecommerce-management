using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CourierConfiguration : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.HasMany(d => d.Shipments)
                .WithOne(shp => shp.Courier)
                .HasForeignKey(shp => shp.CourierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.User)
           .WithOne()
           .HasForeignKey<Courier>(c=>c.UserId)
           .OnDelete(DeleteBehavior.NoAction);

           
        }
    }
}
