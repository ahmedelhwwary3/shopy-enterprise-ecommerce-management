using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class ShippingProviderConfiguration : IEntityTypeConfiguration<ShippingProvider>
    {
        public void Configure(EntityTypeBuilder<ShippingProvider> builder)
        {
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.HasMany(p => p.Couriers)
                .WithOne(d => d.ShippingProvider)
                .HasForeignKey(d => d.ShippingProviderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
