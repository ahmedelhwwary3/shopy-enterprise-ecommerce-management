using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations.Identity
{
    public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
    {
        private string GetPermission(enPermissions permission)
        {
            return permission.ToString().Replace('_','.');
        }
        private string GetModule(enPermissions permission)
        {
            var array = permission.ToString().Split('_');
            return array[0];
        }
        public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
        {
            builder.HasQueryFilter(p => p.IsActive);

            builder.Property(p => p.Id)//Fixed
                .ValueGeneratedNever();

            builder.HasIndex(p => p.Code)
                .IsUnique();

            builder.HasIndex(p => p.Id)
                .IsUnique();

            #region Seed
            //   builder.HasData(new List<ApplicationPermission>
            //    {
            //   // Orders
            //   new() { Id = enPermissions.Order_View, Code = GetPermission(enPermissions.Order_View), Module = GetModule(enPermissions.Order_View), Description = "View orders", IsActive = true },
            //   new() { Id = enPermissions.Order_Create, Code = GetPermission(enPermissions.Order_Create), Module = GetModule(enPermissions.Order_Create), Description = "Create new order", IsActive = true },
            //   new() { Id = enPermissions.Order_Edit, Code = GetPermission(enPermissions.Order_Edit), Module = GetModule(enPermissions.Order_Edit), Description = "Edit order", IsActive = true },
            //   new() { Id = enPermissions.Order_Cancel, Code = GetPermission(enPermissions.Order_Cancel), Module = GetModule(enPermissions.Order_Cancel), Description = "Cancel order", IsActive = true },
            //   new() { Id = enPermissions.Order_ChangeStatus, Code = GetPermission(enPermissions.Order_ChangeStatus), Module = GetModule(enPermissions.Order_ChangeStatus), Description = "Change order status", IsActive = true },

            //   // Customers
            //   new() { Id = enPermissions.Customer_View, Code = GetPermission(enPermissions.Customer_View), Module = GetModule(enPermissions.Customer_View), Description = "View customers", IsActive = true },
            //   new() { Id = enPermissions.Customer_Create, Code = GetPermission(enPermissions.Customer_Create), Module = GetModule(enPermissions.Customer_Create), Description = "Create customer", IsActive = true },
            //   new() { Id = enPermissions.Customer_Edit, Code = GetPermission(enPermissions.Customer_Edit), Module = GetModule(enPermissions.Customer_Edit), Description = "Edit customer", IsActive = true },
            //   new() { Id = enPermissions.Customer_ToggleStatus, Code = GetPermission(enPermissions.Customer_ToggleStatus), Module = GetModule(enPermissions.Customer_ToggleStatus), Description = "Activate or deactivate customer", IsActive = true },

            //   // Products
            //   new() { Id = enPermissions.Product_View, Code = GetPermission(enPermissions.Product_View), Module = GetModule(enPermissions.Product_View), Description = "View products", IsActive = true },
            //   new() { Id = enPermissions.Product_Create, Code = GetPermission(enPermissions.Product_Create), Module = GetModule(enPermissions.Product_Create), Description = "Create product", IsActive = true },
            //   new() { Id = enPermissions.Product_Edit, Code = GetPermission(enPermissions.Product_Edit), Module = GetModule(enPermissions.Product_Edit), Description = "Edit product", IsActive = true },
            //   new() { Id = enPermissions.Product_ToggleStatus, Code = GetPermission(enPermissions.Product_ToggleStatus), Module = GetModule(enPermissions.Product_ToggleStatus), Description = "Activate or deactivate product", IsActive = true },

            //   // Users
            //   new() { Id = enPermissions.User_View, Code = GetPermission(enPermissions.User_View), Module = GetModule(enPermissions.User_View), Description = "View users", IsActive = true },
            //   new() { Id = enPermissions.User_Create, Code = GetPermission(enPermissions.User_Create), Module = GetModule(enPermissions.User_Create), Description = "Create user", IsActive = true },
            //   new() { Id = enPermissions.User_Edit, Code = GetPermission(enPermissions.User_Edit), Module = GetModule(enPermissions.User_Edit), Description = "Edit user", IsActive = true },
            //   new() { Id = enPermissions.User_ToggleStatus, Code = GetPermission(enPermissions.User_ToggleStatus), Module = GetModule(enPermissions.User_ToggleStatus), Description = "Activate or deactivate user", IsActive = true },

            //   // Roles
            //   new() { Id = enPermissions.Role_View, Code = GetPermission(enPermissions.Role_View), Module = GetModule(enPermissions.Role_View), Description = "View roles", IsActive = true },
            //   new() { Id = enPermissions.Role_Create, Code = GetPermission(enPermissions.Role_Create), Module = GetModule(enPermissions.Role_Create), Description = "Create role", IsActive = true },
            //   new() { Id = enPermissions.Role_Edit, Code = GetPermission(enPermissions.Role_Edit), Module = GetModule(enPermissions.Role_Edit), Description = "Edit role and permissions", IsActive = true },
            //   new() { Id = enPermissions.Role_AssignPermissions, Code = GetPermission(enPermissions.Role_AssignPermissions), Module = GetModule(enPermissions.Role_AssignPermissions), Description = "Assign permissions to role", IsActive = true },

            //   // Permissions
            //   new() { Id = enPermissions.Permission_View, Code = GetPermission(enPermissions.Permission_View), Module = GetModule(enPermissions.Permission_View), Description = "View system permissions", IsActive = true },
            //}); 
            #endregion

        }
    }
}
