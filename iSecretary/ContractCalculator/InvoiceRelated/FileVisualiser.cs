using System;
using ContractStatisticsAnalyser;

namespace UserInterface.InvoiceRelated
{
    public class FileVisualiser
    {
        public static void VisualiseIfRequested(string pdfFileName)
        {
            if (InputReceiver.GetBool("View local copy?"))
            {
                PdfVisualiser.Show(pdfFileName);
            }
            Console.WriteLine("");
        }
    }
}