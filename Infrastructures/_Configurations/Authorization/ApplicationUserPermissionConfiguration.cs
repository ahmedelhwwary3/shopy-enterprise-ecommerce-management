using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class ApplicationUserPermissionConfiguration :
        IEntityTypeConfiguration<ApplicationUserPermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserPermission> builder)
        {
            builder.HasOne(userPRM => userPRM.User)
                .WithMany(user=>user.UserPermissions)
                .HasForeignKey(userPRM=>userPRM.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(userPRM => userPRM.Permission)
                .WithMany(prm => prm.UserPermissions)
                .HasForeignKey(userPRM => userPRM.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
