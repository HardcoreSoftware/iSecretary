using System.Text;
using iTextSharp.text;

namespace Invoices
{
    public class ClientInfoFactory
    {
        public static void Create(Document document, string clientAddress, string commentsOrSpecialInstructions)
        {
            document.Add(ClientAddress(clientAddress));
            document.Add(SpecialInstructions(commentsOrSpecialInstructions));
        }

        private static IElement ClientAddress(string clientAddress)
        {
            var p = new Phrase("\n\nTO:\n", ElementFactory.StandardFontBold);
            var p2 = new Phrase(clientAddress, ElementFactory.StandardFont);
            var x = new Paragraph() { Leading = 0, MultipliedLeading = 0.8f };
            x.Add(p);
            x.Add(p2);
            return x;
        }

        private static IElement SpecialInstructions(string commentsOrSpecialInstructions)
        {
            var c = new Phrase("\nCOMMENTS OR SPECIAL INSTRUCTIONS:\n", ElementFactory.StandardFontBold);
            var c1 = ElementFactory.GetPhrase(commentsOrSpecialInstructions, ElementFactory.Fonts.Standard);
            var c2 = new Phrase("\n\n\n", ElementFactory.StandardFont);
            var p = new Paragraph { c, c1, c2 };
            return p;
        }
    }
}