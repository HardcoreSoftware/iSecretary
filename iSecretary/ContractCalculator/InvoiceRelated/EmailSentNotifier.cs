using System;
using Data.Invoice;

namespace UserInterface.InvoiceRelated
{
    public class EmailSentNotifier
    {
        public static void ShowSentToClient(ClientEntity clientTo)
        {
            Console.WriteLine("Sent to: {0} ({1})\n\n", clientTo.PointOfContactEmail, clientTo.PointOfContactName);
        }
    }
}