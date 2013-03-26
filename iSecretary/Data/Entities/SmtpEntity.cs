using System.Net;
using System.Net.Mail;

namespace Data.Entities
{
    public class SmtpEntity : IEntity
    {
        public string Host { get; set; }
        public MailAddress FromAsMailAddress { get { return new MailAddress(From); } }
        public string From { get; set; }
        public string CredentialsAccount { get; set; }
        public string CredentialsPassword { get; set; }
        public NetworkCredential Credentials { get { return new NetworkCredential(CredentialsAccount, CredentialsPassword); } }
    }
}
