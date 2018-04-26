using System;
using System.Linq;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using web.Models;

namespace web.Services
{
    public class EMailService : IMailService
    {
        private readonly ILogger<EMailService> _logger;
        private readonly IEmailConfiguration _emailConfiguration;
 
        public EMailService(IEmailConfiguration emailConfiguration, ILogger<EMailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }

        public void SendMail(EmailModel emailModel)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailModel.Customers.Select(x => new MailboxAddress(x.ApplicationUserFirstName + x.ApplicationUserLastName, x.ApplicationUserEmail)));
            message.From.Add(new MailboxAddress(emailModel.EmailFromName, emailModel.EmailFromAddress));
 
            message.Subject = emailModel.EmailSubject;
            message.Body = new TextPart(TextFormat.Text)
            {
                Text = emailModel.EmailBody
            };
 
            // Need to use what user send back to us when filling out the send email form for now
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(emailModel.EmailServer, emailModel.EmailPort, false);
 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
 
                emailClient.Authenticate(emailModel.EmailUsername, emailModel.EmailSenderPassword);
 
                emailClient.Send(message);
 
                emailClient.Disconnect(true);
            }
        }
    }
}