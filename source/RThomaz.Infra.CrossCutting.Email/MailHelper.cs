using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace RThomaz.Infra.CrossCutting.Email
{
    public static class MailHelper
    {
        public static void SendMail(string recipients, string subject, string body)
        {
            Execute(recipients, subject, body).Wait();
        }

        private static async Task Execute(string recipients, string subject, string body)
        {
            try
            {
                var apiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                var fromEmail = ConfigurationManager.AppSettings["SendGrid.FromEmail"];

                var client = new SendGridClient(apiKey);

                EmailAddress from = new EmailAddress(fromEmail);

                EmailAddress to = new EmailAddress(recipients);

                var plainTextContent = "and easy to do anywhere, even with C#";

                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                
                var msg = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}