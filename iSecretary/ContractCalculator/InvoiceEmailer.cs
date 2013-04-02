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
        public static void SendEmailWithAttachement(ClientEntity clientEntity, EmailEntity emailEntity, IInvoiceDetails invoiceDetails, string pdfFileName, DateTime now)
        {
            var subject = "Invoice #" + InvoiceNameGenerator.GetName(invoiceDetails.Number, now);
            var sender = new Sender(new DefaultSmtpWrapper().Data);

            var body = EmailBodyCreator.Create(emailEntity, clientEntity.PointOfContactName);

            sender.Send(clientEntity.PointOfContactEmail, subject, body, new List<string> { pdfFileName });
        }

        public static void EmailIfRequested(Repository repository, string pdfFilename, IInvoiceDetails invoiceDetails, DateTime now)
        {
            var send = UserInputRetriever.GetBool("Do you want to email this invoice to a client?");

            if (send)
            {
                var emailTarget = ClientSelector.Get(repository.ClientsWrapper.Data);
                SendEmailWithAttachement(emailTarget, repository.EmailWrapper.Data, invoiceDetails, pdfFilename, now);
                EmailSentNotifier.ShowSentToClient(emailTarget);
            }

            Console.WriteLine("");
        }
    }
}