using System;
using ContractStatisticsAnalyser;

namespace UserInterface.InvoiceRelated
{
    public class DirectoryVisualisationRequester
    {
        public static void VisualiseIfRequested(string pdfFilename)
        {
            if (UserInputRetriever.GetBool("View directory?"))
            {
                DirectoryVisualiser.ShowFile(pdfFilename);
            }
            Console.WriteLine("");
        }
    }
}