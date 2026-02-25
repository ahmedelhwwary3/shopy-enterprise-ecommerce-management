using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class AspNetUserTokenConfiguration:IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(ut => ut.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
