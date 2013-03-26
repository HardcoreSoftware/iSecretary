using System;
using System.Collections.Generic;
using ContentProvider;
using ContractStatisticsAnalyser;
using Data;
using Data.Entities;
using Data.EntityWrappers.Smtp;
using Data.Invoice;
using EmailProvider;
using Invoices;
using UserInterface.InvoiceRelated;

namespace UserInterface
{
    public class InvoiceEmailer
    {
        public static void SendEmailWithAttachement(ClientEntity clientEntity, EmailEntity emailEntity, IInvoiceDetails invoiceDetails, string pdfFileName)
        {
            var subject = "Invoice #" + InvoiceNameGenerator.GetName(invoiceDetails.Number, DateTime.Now);
            var sender = new Sender(new DefaultSmtpWrapper().Data);

            var body = EmailBodyCreator.Create(emailEntity, clientEntity.PointOfContactName);

            sender.Send(clientEntity.PointOfContactEmail, subject, body, new List<string> { pdfFileName });
        }

        public static void EmailIfRequested(Repository repository, string pdfFilename, IInvoiceDetails invoiceDetails)
        {
            var send = InputReceiver.GetBool("Do you want to email this invoice to a client?");

            if (send)
            {
                var emailTarget = ClientSelector.Get(repository.ClientsWrapper.Data);
                SendEmailWithAttachement(emailTarget, repository.EmailWrapper.Data, invoiceDetails, pdfFilename);
                EmailSentNotifier.ShowSentToClient(emailTarget);
            }

            Console.WriteLine("");
        }
    }
}