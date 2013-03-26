using System;
using System.Globalization;
using Data.Entities;
using Data.Invoice;
using Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Invoices
{
    public class CostSummaryFactory
    {
        const string CurrencyString = "#,##0.00";

        public static void CreateWeekly(Document document, WeeklyInvoiceDetails wid, InvoiceEntity invoiceEntity, DateTime now)
        {
            var table = new PdfPTable(4) { WidthPercentage = 100 };
            var colWidthPercentages = new[] { 1f, 3f, 1f, 1f };
            table.SetWidths(colWidthPercentages);

            var subTotal = wid.ChargeableHours * wid.HourlyRate;

            AddHeader(table);
            AddWeekWorkCost(table, wid, invoiceEntity,now);
            AddSummary(table, subTotal, subTotal * 0.2);

            document.Add(table);
        }

        public static void CreateCustom(Document document, SimpleInvoiceDetails simpleInvoiceDetails, InvoiceEntity invoiceEntity)
        {
            var table = new PdfPTable(4) { WidthPercentage = 100 };
            var colWidthPercentages = new[] { 1f, 3f, 1f, 1f };
            table.SetWidths(colWidthPercentages);

            var subTotal = simpleInvoiceDetails.Quantity * simpleInvoiceDetails.UnitPrice;


            AddHeader(table);
            AddCustomCost(table, simpleInvoiceDetails, subTotal);
            AddSummary(table, subTotal, subTotal * 0.2);

            document.Add(table);
        }

        private static void AddHeader(PdfPTable table)
        {
            table.AddCell(ElementFactory.CreateCell("QUANTITY"));
            table.AddCell(ElementFactory.CreateCell("DESCRIPTION"));
            table.AddCell(ElementFactory.CreateCell("UNIT PRICE"));
            table.AddCell(ElementFactory.CreateCell("TOTAL", Element.ALIGN_RIGHT));
        }


        private static void AddCustomCost(PdfPTable table, SimpleInvoiceDetails simpleInvoiceDetails, double subTotal)
        {
            table.AddCell(ElementFactory.CreateCell(simpleInvoiceDetails.Quantity.ToString(CultureInfo.InvariantCulture)));

            table.AddCell(ElementFactory.CreateCell(simpleInvoiceDetails.Description, Element.ALIGN_LEFT, 200));

            table.AddCell(ElementFactory.CreateCell(string.Format("{0}", simpleInvoiceDetails.UnitPrice)));

            table.AddCell(ElementFactory.CreateCell(subTotal.ToString(CurrencyString), Element.ALIGN_RIGHT));
        }

        private static void AddWeekWorkCost(PdfPTable table, WeeklyInvoiceDetails wid, InvoiceEntity invoiceEntity, DateTime now)
        {
            table.AddCell(ElementFactory.CreateCell(wid.ChargeableHours.ToString(CultureInfo.InvariantCulture)));

            table.AddCell(ElementFactory.CreateCell(
                string.Format("{0} ({1} - {2})",
                invoiceEntity.ChargeableJob.Description,
                now.ToStartOfBusinessWeek().ToString("dd MMM yyyy"),
                now.ToEndOfBusinessWeek().ToString("dd MMM yyyy")), Element.ALIGN_LEFT, 200));

            table.AddCell(ElementFactory.CreateCell(string.Format("{0}", wid.HourlyRate)));

            table.AddCell(ElementFactory.CreateCell((wid.HourlyRate * wid.ChargeableHours).ToString(CurrencyString), Element.ALIGN_RIGHT));
        }

        private static void AddSummary(PdfPTable table, double subTotal, double vat)
        {
            var sTot0 = ElementFactory.CreateCell("SUBTOTAL:", Element.ALIGN_RIGHT, -1, BaseColor.BLACK, 3);
            sTot0.Border = Rectangle.TOP_BORDER;

            var sTot1 = ElementFactory.CreateCell(subTotal.ToString(CurrencyString), Element.ALIGN_RIGHT, -1, BaseColor.BLACK);
            sTot1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;

            var vat0 = ElementFactory.CreateCell("VAT (20%):", Element.ALIGN_RIGHT, -1, BaseColor.WHITE, 3);
            var vat1 = ElementFactory.CreateCell(vat.ToString(CurrencyString), Element.ALIGN_RIGHT, -1, BaseColor.BLACK);
            vat1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;

            var tot0 = ElementFactory.CreateCell("TOTAL:", Element.ALIGN_RIGHT, -1, BaseColor.WHITE, 3);
            var tot1 = ElementFactory.CreateCell((subTotal + vat).ToString(CurrencyString), Element.ALIGN_RIGHT);

            table.AddCell(sTot0);
            table.AddCell(sTot1);
            table.AddCell(vat0);
            table.AddCell(vat1);
            table.AddCell(tot0);
            table.AddCell(tot1);
        }
    }
}