using System;
using System.Text;
using Data.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Invoices
{
    public class HeaderFactory
    {
        public static void Create(IElementListener document, int invoiceNumber, InvoiceEntity invoiceEntity, CompanyInformationEntity companyInformationEntity, DateTime now)
        {
            var table = new PdfPTable(2) { WidthPercentage = 100 };
            table.AddCell(GetCompanyNameAndSlogan(invoiceEntity, companyInformationEntity));
            table.AddCell(GetInvoiceTitle());
            table.AddCell(GetCompanyAddress(invoiceEntity, companyInformationEntity));
            table.AddCell(GetInvoiceDetails(invoiceNumber,now));
            document.Add(table);
        }

        public static PdfPCell GetCompanyNameAndSlogan(InvoiceEntity invoiceEntity, CompanyInformationEntity companyInformationEntity)
        {
            var p = new Paragraph { Alignment = Element.ALIGN_TOP };
            p.Add(ElementFactory.GetParagraph(companyInformationEntity.Name, ElementFactory.Fonts.Large));
            p.Add(ElementFactory.GetParagraph(companyInformationEntity.Slogan, ElementFactory.Fonts.Standard));
            var c = new PdfPCell(p)
                {
                    VerticalAlignment = Element.ALIGN_TOP,
                    FixedHeight = 50f,
                    PaddingTop = 0,
                    BorderColor = BaseColor.WHITE
                };
            return c;
        }

        public static PdfPCell GetInvoiceTitle()
        {
            var p = ElementFactory.GetPhrase("INVOICE", ElementFactory.Fonts.ExtraLarge);
            p.Font.Color = BaseColor.GRAY;
            var c = new PdfPCell(p)
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    PaddingTop = 0,
                    BorderColor = BaseColor.WHITE
                };
            return c;
        }

        public static PdfPCell GetCompanyAddress(InvoiceEntity invoiceEntity, CompanyInformationEntity companyInformationEntity)
        {
            var s = new StringBuilder();
            s.AppendLine(string.Format("{0}, {1}", companyInformationEntity.AddressLine1, companyInformationEntity.PostalTown));
            s.AppendLine(string.Format("{0}, {1}", companyInformationEntity.PostCode, companyInformationEntity.Country));
            s.AppendLine(companyInformationEntity.WebsiteUrl);
            s.AppendLine(string.Format("{0} | {1}", companyInformationEntity.CellPhone, companyInformationEntity.OfficePhone));
            return new PdfPCell(ElementFactory.GetParagraph(s.ToString(), ElementFactory.Fonts.Compact))
            {
                VerticalAlignment = Element.ALIGN_BOTTOM,
                BorderColor = BaseColor.WHITE
            };
        }

        public static PdfPCell GetInvoiceDetails(int invoiceNumber, DateTime now)
        {
            var c1 = new Chunk("INVOICE #: ", ElementFactory.StandardFontBold);
            var c2 = new Chunk(InvoiceNameGenerator.GetName(invoiceNumber, now), ElementFactory.StandardFont);
            var c3 = new Chunk("\nDATE: ", ElementFactory.StandardFontBold);
            var c4 = new Chunk(string.Format("{0}", now.ToString("dd MMMM yyyy").ToUpper()), ElementFactory.StandardFont);

            var p = new Paragraph { c1, c2, c3, c4 };

            var c = new PdfPCell(p)
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    BorderColor = BaseColor.WHITE
                };
            return c;
        }
    }
}