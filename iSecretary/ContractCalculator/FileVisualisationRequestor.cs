using System;
using ContractStatisticsAnalyser;

namespace UserInterface
{
    public class FileVisualisationRequestor
    {
        public static void VisualiseIfRequested(string pdfFileName)
        {
            if (UserInputRetriever.GetBool("View local copy?"))
            {
                FileVisualiser.Show(pdfFileName);
            }
            Console.WriteLine("");
        }
    }
}