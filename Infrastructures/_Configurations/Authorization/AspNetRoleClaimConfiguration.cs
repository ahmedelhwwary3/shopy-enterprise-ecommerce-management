using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class AspNetRoleClaimConfiguration:IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
