using System.Net.Mail;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;

namespace FINALPROJECT.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 465;
        string password =  "etmy pdrw mped ixlu";
        string senderEmail = "adekolahazeeb3@gmail.com";
        public void SendEMail(EmailRequestModel mailRequest)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Carsz", senderEmail));
            message.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            message.Subject = mailRequest.Subject;

            var body = new TextPart("html")
            {
                Text = mailRequest.HtmlContent,
            };
            message.Body = body;

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(smtpServer, smtpPort, true);
                client.Authenticate(senderEmail, password);
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email.", ex);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
