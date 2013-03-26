using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Data.Entities;

namespace EmailProvider
{
    public class Sender
    {
        private readonly SmtpEntity _smtpEntity;

        public Sender(SmtpEntity smtpEntity)
        {
            _smtpEntity = smtpEntity;
        }

        public void Send(string to, string subject, string body, List<string> attachementFileNames)
        {
            var message = new MailMessage {IsBodyHtml = true};
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.From = _smtpEntity.FromAsMailAddress;
            var mailer = new SmtpClient
                {
                    Host = _smtpEntity.Host,
                    Credentials = _smtpEntity.Credentials
                };

            if (attachementFileNames != null)
            {
                foreach (var data in attachementFileNames.Select(attachementFileName => new Attachment(attachementFileName, MediaTypeNames.Application.Octet)))
                {
                    message.Attachments.Add(data);
                }
            }
            mailer.Send(message);
        }
    }
}
