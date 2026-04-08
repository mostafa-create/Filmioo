using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(DAL.Models.Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mostafayasseen33@gmail.com", "dcofxsgeejwidjgh");
            Client.Send("mostafayasseen33@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
