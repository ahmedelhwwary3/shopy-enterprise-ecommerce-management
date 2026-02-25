using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures._Configurations
{
    public class CountryDeliveryFeeConfiguration : IEntityTypeConfiguration<CountryDeliveryFee>
    {
        public void Configure(EntityTypeBuilder<CountryDeliveryFee> builder)
        {
            builder.HasOne(d => d.Country)
                .WithOne()
                .HasForeignKey<CountryDeliveryFee>(d => d.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(d=>d.IsActive); 
        }
    }
}
