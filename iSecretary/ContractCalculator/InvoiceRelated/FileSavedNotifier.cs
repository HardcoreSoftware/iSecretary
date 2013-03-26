using System;

namespace UserInterface.InvoiceRelated
{
    public class FileSavedNotifier
    {
        public static void Notify(string filename)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Saved to " + filename);
            Console.WriteLine(" ");
        }
    }
}