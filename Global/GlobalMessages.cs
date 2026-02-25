using Microsoft.AspNetCore.Http.HttpResults;

namespace Enterprise_E_Commerce_Management_System.Global
{
    public static class GlobalMessages
    {
        public static string SuccessMessage => "SuccessMessage";
        public static string UpdatedSuccessfully(string domain)
            => $"{domain} edit succeeded.";

        public static string DeletedSuccessfully(string domain)
            => $"{domain} deleted successfully.";
        public static string CreateSucceeded(string domain)
            => $"{domain} create succeeded.";
    }
}
