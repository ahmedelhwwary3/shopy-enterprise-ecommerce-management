using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasQueryFilter(u => u.IsActive);

            builder.HasOne(u => u.Country)
                .WithMany()
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(ord => ord.CountryId)
               .HasDefaultValue(1);
        }
    }
}
