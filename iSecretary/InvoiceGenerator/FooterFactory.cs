using Data.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Invoices
{
    public class FooterFactory
    {
        public static void Create(Document document, InvoiceEntity invoiceEntity)
        {
            document.Add(ElementFactory.GetParagraph("\n\n\n\n\n" + invoiceEntity.FooterText + "\n\n\n", ElementFactory.Fonts.Footer, Element.ALIGN_JUSTIFIED));
            AddPaymentDetails(document, invoiceEntity);
        }

        private static void AddPaymentDetails(Document document, InvoiceEntity invoiceEntity)
        {
            var table = new PdfPTable(3) { WidthPercentage = 45 };
            var colWidthPercentages = new[] { 4f, 1f, 5f };
            table.SetWidths(colWidthPercentages);

            table.AddCell(ElementFactory.CreateCell("Name of Account", Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(":", Element.ALIGN_CENTER, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(invoiceEntity.AccountDetails.Name, Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell("Account Number:", Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(":", Element.ALIGN_CENTER, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(invoiceEntity.AccountDetails.Number, Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell("Sort Code", Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(":", Element.ALIGN_CENTER, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            table.AddCell(ElementFactory.CreateCell(invoiceEntity.AccountDetails.SortCode, Element.ALIGN_LEFT, -1, BaseColor.WHITE, 1, ElementFactory.Fonts.Footer));
            document.Add(table);
        }
    }
}