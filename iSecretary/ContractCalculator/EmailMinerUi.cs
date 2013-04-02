using System;
using System.Collections.Generic;
using ContractStatisticsAnalyser;
using Data;
using DataMiner.MozillaThunderbird;

namespace UserInterface
{
    public class EmailMinerUi
    {
        public static void Run(Repository repo, string uniqueEmailaddresses, string uniqueDomains, string badFilename)
        {
            List<string> badResults;
            var goodResults = Extractor.GetEmailAddresses(repo.StorageWrapper.Data.EmailExportDirectory, out badResults, null);


            var directory = repo.StorageWrapper.Data.EmailDataMiningResultsDirectory;
            var fullPath = directory + uniqueEmailaddresses;

            Console.WriteLine("Saving...");
            DataWriter.WriteAll(directory, uniqueEmailaddresses, badFilename, uniqueDomains, goodResults, badResults);

            Console.WriteLine("A total of {0} email addresses saved to - {1}\n", goodResults.Count, fullPath);

            if (UserInputRetriever.GetBool(string.Format("View {0}?", uniqueEmailaddresses)))
            {
                FileVisualiser.Show(fullPath);
            }

            if (UserInputRetriever.GetBool(string.Format("View {0}?", directory)))
            {
                DirectoryVisualiser.ShowFile(fullPath);
            }
        }
    }
}