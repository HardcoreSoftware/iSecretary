using System;
using System.IO;
using Data.Entities;
using Data.Invoice;
using IOInteraction;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Invoices
{
    public class Generator
    {
        /// <summary>
        /// Returns the file name of the newly creasted invoice
        /// </summary>
        /// <param name="invoiceEntity"></param>
        /// <param name="wid"></param>
        /// <param name="client"></param>
        /// <param name="companyInformationEntity"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public string CreateWeeklyInvoice(InvoiceEntity invoiceEntity, WeeklyInvoiceDetails wid, ClientEntity client, CompanyInformationEntity companyInformationEntity, DateTime now,string invoiceFolder)
        {
            DirectoryCreator.EnsureExistance(invoiceFolder);

            var pdfDoc = new Document(PageSize.A4, 50, 50, 25, 25);

            var pdfFileName = FileNameProvider.GetAvailableFileName(invoiceFolder + "\\Invoice-", InvoiceNameGenerator.GetName(wid.Number, now), ".pdf");

            var output = new FileStream(pdfFileName, FileMode.OpenOrCreate);

            PdfWriter.GetInstance(pdfDoc, output);

            pdfDoc.Open();

            HeaderFactory.Create(pdfDoc, wid.Number, invoiceEntity, companyInformationEntity, now);

            ClientInfoFactory.Create(pdfDoc, CompositeAddressCreator.CreateAddress(client.CompanyInformationEntity), wid.CommentsOrSpecialInstructions);

            CostSummaryFactory.CreateWeekly(pdfDoc, wid, invoiceEntity,now);

            FooterFactory.Create(pdfDoc, invoiceEntity);

            pdfDoc.Close();

            return pdfFileName;
        }

        public string CreateCustomInvoice(InvoiceEntity invoiceEntity, SimpleInvoiceDetails simpleInvoiceDetails, CompanyInformationEntity companyInformationEntity, DateTime now, string invoiceFolder)
        {
            DirectoryCreator.EnsureExistance(invoiceFolder);

            var pdfFileName = FileNameProvider.GetAvailableFileName(invoiceFolder + "\\Invoice-", InvoiceNameGenerator.GetName(simpleInvoiceDetails.Number, now), ".pdf");
            
            var pdfDoc = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new FileStream(pdfFileName, FileMode.OpenOrCreate);
            PdfWriter.GetInstance(pdfDoc, output);

            pdfDoc.Open();

            HeaderFactory.Create(pdfDoc, simpleInvoiceDetails.Number, invoiceEntity, companyInformationEntity,now);

            ClientInfoFactory.Create(pdfDoc, CompositeAddressCreator.CreateAddress(simpleInvoiceDetails.Client.CompanyInformationEntity), simpleInvoiceDetails.CommentsOrSpecialInstructions);

            CostSummaryFactory.CreateCustom(pdfDoc, simpleInvoiceDetails, invoiceEntity);
            FooterFactory.Create(pdfDoc, invoiceEntity);

            pdfDoc.Close();

            return pdfFileName;
        }
    }

    public class FileNameProvider
    {
        public static string GetAvailableFileName(string folder, string idealName, string extension)
        {
            var tryAgain = true;
            var copyIndex = 0;
            var suffix = "";
            var pdfFileName = "";
            while (tryAgain)
            {
                pdfFileName = folder + idealName + suffix + extension;
                if (!File.Exists(pdfFileName))
                {
                    tryAgain = false;
                }
                else
                {
                    tryAgain = true;
                    copyIndex++;
                    suffix = string.Format(" ({0})", copyIndex);
                }
            }
            return pdfFileName;
        }
    }
}
