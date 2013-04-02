using System;
using System.Linq;
using Data.EntityWrappers;
using Data.EntityWrappers.Clients;
using Data.EntityWrappers.CompanyInformation;
using Data.EntityWrappers.Email;
using Data.EntityWrappers.Smtp;
using Data.EntityWrappers.WeeklyInvoice;
using Data.Invoice;
using EmailProvider;
using Invoices;
using NUnit.Framework;
using UserInterface;

namespace Tests
{
    [TestFixture]
    class EmailSenderTests
    {
        [Test]
        public void SendEmail()
        {
            var sender = new Sender(new DefaultSmtpWrapper().Data);
            Assert.DoesNotThrow(() => sender.Send("murados91@gmail.com", "Subject Line Test", "Body Test", null));
        }


        [Test]
        public void TestInvoiceSender()
        {
            var clientTo = new DefaultClientsWrappers().Data.First();
            var emailConfig = new DefaultEmailWrapper().Data;
            var ic = new DefaultInvoiceWrapper().Data;
            var icd = new WeeklyInvoiceDetails
                {
                    ChargeableHours = 37.5,
                    Number = 21,
                    HourlyRate = 44,
                    CommentsOrSpecialInstructions = "This is a test invoice - there is no need to take action.",
                };

            var client = new TestClientsWrappers();

            var generator = new Generator();
            var filename = generator.CreateWeeklyInvoice(new TestInvoiceWrapper().Data, icd, client.Data.First(), new DefaultCompanyInformationWrapper().Data, DateTime.Now, "C:\\Hardcore Software\\iSec\\Invoices\\");
            Assert.DoesNotThrow(() => InvoiceEmailer.SendEmailWithAttachement(clientTo, emailConfig, icd, filename, DateTime.Now));
        }
    }
}