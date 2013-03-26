using System;
using ContractStatisticsAnalyser;

namespace UserInterface.InvoiceRelated
{
    public class DirectoryVisualisationProvider
    {
        public static void VisualiseIfRequested(string pdfFilename)
        {
            if (InputReceiver.GetBool("View directory?"))
            {
                DirectoryVisualiser.ShowFile(pdfFilename);
            }
            Console.WriteLine("");
        }
    }
}