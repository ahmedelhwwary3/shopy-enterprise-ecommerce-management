using AutoMapper;
using Azure.Core;
using Enterprise_E_Commerce_Management_System.Application.Emails.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.Identity.Client;
using System.Net;
using System.Net.Mail;
namespace Enterprise_E_Commerce_Management_System.Application.Emails
{
    public class EmailSerivce : IEmailSerivce
    {
        public enSendEmailResult SendOrderEmail(string toEmail, string token)
        {
            if (string.IsNullOrEmpty(toEmail) ||
                string.IsNullOrEmpty(token))
                return enSendEmailResult.InvalidData;

            //var trackingLink =
            //$"https://shopy.com/Order/Track?AccessToken={token}";

            //var message = new MailMessage();
            //message.From = new MailAddress("no-reply@shopy.com", "Shopy");
            //message.To.Add(toEmail); // Simulation
            //message.Subject = "Order Confirmation – Shopy";

            //message.Body =
            //$@"
            //   Hi,
               
            //   Thank you for your order! 🎉
               
            //   Your order has been successfully confirmed and is now being processed.
               
            //   You can view your order details and track its status using the link below:
            //   {trackingLink}
               
            //   Please keep this email for your records.
               
            //   If you have any questions, feel free to contact our support team.
               
            //   Best regards,
            //   Shopy Team
            //   ";

            //message.IsBodyHtml = false;

            //var smtp = new SmtpClient("sandbox.smtp.mailtrap.io", 587);
            //smtp.EnableSsl = true;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new NetworkCredential(
            //    "d276577872aee2",
            //    "ddc24488046c30"
            //);

            //smtp.Send(message);
            return enSendEmailResult.Success;
        }
    }

}