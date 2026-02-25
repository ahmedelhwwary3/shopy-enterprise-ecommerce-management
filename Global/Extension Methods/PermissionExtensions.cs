namespace Enterprise_E_Commerce_Management_System.Global.Extension_Methods
{
    public static class PermissionExtensions
    {
        public static string GetPermissionCode(this enPermissions p)
        {
            return p.ToString().Replace('_', '.');
        }
    }
}
