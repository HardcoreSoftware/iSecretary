using System;
using ContractStatisticsAnalyser;

namespace UserInterface.InvoiceRelated
{
    public class DirectoryVisualisationRequester
    {
        public static void VisualiseIfRequested(string pdfFilename)
        {
            if (UIRetriever.GetBool("View directory?"))
            {
                DirectoryVisualiser.ShowFile(pdfFilename);
            }
            Console.WriteLine("");
        }
    }
}