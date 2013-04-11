using System;
using ContractStatisticsAnalyser;

namespace UserInterface
{
    public class FileVisualisationRequestor
    {
        public static void VisualiseIfRequested(string pdfFileName)
        {
            if (UIRetriever.GetBool("View local copy?"))
            {
                FileVisualiser.Show(pdfFileName);
            }
            Console.WriteLine("");
        }
    }
}