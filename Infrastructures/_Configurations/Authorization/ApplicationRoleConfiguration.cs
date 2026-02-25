using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
             

            builder.HasData(new List<ApplicationRole>
            {
                new()
                {
                    Id = RoleIDs.Admin,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "3e6f9c4a-7d2b-4c8f-9a41-8c1f2b7a01a1"
                },
                new()
                {
                    Id = RoleIDs.Manager,
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = "91b4e2d7-5f3c-4a1b-8e62-0a9c3d4f02b2"
                },
                new()
                {
                    Id = RoleIDs.Support,
                    Name = "Support",
                    NormalizedName = "SUPPORT",
                    ConcurrencyStamp = "c7a1f3e9-8b6d-4c52-9d14-6f2e8a3b03c3"
                },
                new()
                {
                    Id = RoleIDs.Staff,
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = "f2d9a8b4-1e7c-4f63-8a25-b1c6e4d904d4"
                },new()
                {
                    Id = RoleIDs.Courier,
                    Name = "Courier",
                    NormalizedName = "COURIER",
                    ConcurrencyStamp =  "f2d9a8b4-1e7c-4f63-8a25-b1c6e2d000d4" // لو عندك الفلاج ده
                }

            });
  
        }
    }
}
