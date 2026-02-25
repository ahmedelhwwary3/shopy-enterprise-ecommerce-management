using Enterprise_E_Commerce_Management_System.Application.Emails.Results;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;

namespace Enterprise_E_Commerce_Management_System.Application.Emails
{
    public interface IEmailSerivce
    {
        enSendEmailResult SendOrderEmail(string toEmail, string accessToken);
    }
}
