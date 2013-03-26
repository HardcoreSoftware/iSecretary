using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Invoices
{
    public class ElementFactory
    {
        public readonly static Font ExtraLargeFont = FontFactory.GetFont("Verdana", 26, Font.BOLD, BaseColor.GRAY);
        public readonly static Font StandardFont = new Font { Size = 8 };
        public readonly static Font HyperlinkFont = new Font { Size = 8,Color = BaseColor.BLUE};
        public readonly static Font FooterFont = FontFactory.GetFont("Verdana", 8, Font.NORMAL, BaseColor.GRAY);
        public readonly static Font StandardFontBold = FontFactory.GetFont("Verdana", 8, Font.BOLD);

        public enum Fonts
        {
            Large,
            Compact,
            Standard,
            ExtraLarge,
            Footer
        }

        public static Phrase GetPhrase(string text, Fonts type)
        {
            Phrase p;
            switch (type)
            {
                case Fonts.Large: p = new Phrase(text); break;
                case Fonts.Footer: p = new Phrase(text, FooterFont); break;
                case Fonts.Standard: p = new Phrase(text, StandardFont); break;
                case Fonts.ExtraLarge:
                    p = new Phrase(text, ExtraLargeFont);
                    break;
                default: throw new ArgumentOutOfRangeException("type");
            }
            return p;
        }

        public static Paragraph GetParagraph(string text, Fonts type, int align = Element.ALIGN_LEFT)
        {
            Paragraph p;
            switch (type)
            {
                case Fonts.Footer:
                    p = new Paragraph(text, FooterFont);
                    break;

                case Fonts.ExtraLarge:
                    p = new Paragraph(text, ExtraLargeFont);
                    break;

                case Fonts.Large:
                    p = new Paragraph(text);
                    break;

                case Fonts.Compact:
                    p = new Paragraph(text, StandardFont);
                    p.SetLeading(0, 0.8f);
                    break;

                case Fonts.Standard:
                    p = new Paragraph(text, StandardFont);
                    break;

                default: throw new ArgumentOutOfRangeException("type");
            }

            p.Alignment = align;

            return p;
        }


        public static PdfPCell CreateCell(string str, int align = Element.ALIGN_CENTER, float fixedHeight = -1, BaseColor borderColor = null, int colSpan = 1, Fonts font = Fonts.Standard)
        {
            var c = new PdfPCell(GetPhrase(str, font))
            {
                HorizontalAlignment = align,
                VerticalAlignment = Element.ALIGN_TOP,
                Padding = 5,
                BorderColor = borderColor ?? BaseColor.BLACK,
                Colspan = colSpan
            };
            if (fixedHeight > 0)
            {
                c.FixedHeight = fixedHeight;
            }
            c.PaddingTop = 2;
            return c;
        }
    }
}