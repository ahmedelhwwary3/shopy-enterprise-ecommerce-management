using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class ApplicationRolePermissionConfigurations : IEntityTypeConfiguration<ApplicationRolePermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationRolePermission> builder)
        {
            builder.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp=>rp.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp=>rp.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasKey(rp=>new { rp.RoleId,rp.PermissionId});//Composite Key

            builder.Property(rp => rp.PermissionId)
                .HasConversion<int>();
             
        }
    }
}
