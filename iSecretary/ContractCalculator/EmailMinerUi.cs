using System;
using System.Collections.Generic;
using ContractStatisticsAnalyser;
using Data;
using DataMiner.MozillaThunderbird;

namespace UserInterface
{
    public class EmailMinerUi
    {
        public static void Run(Repository repo)
        {
            List<string> badResults;
            var goodResults = Extractor.GetEmailAddresses(repo.StorageWrapper.Data.EmailExportDirectory, out badResults, null);

            const string goodFilename = "UniqueEmailAddresses.txt";
            const string domainFilename = "UniqueDomains.txt";
            const string badFilename = "Failures.txt";
            
            var directory = repo.StorageWrapper.Data.EmailDataMiningResultsDirectory;
            var fullPath = directory + goodFilename;

            Console.WriteLine("Saving...");
            DataWriter.WriteAll(directory, goodFilename, badFilename, domainFilename, goodResults, badResults);

            Console.WriteLine("A total of {0} email addresses saved to - {1}\n", goodResults.Count, fullPath);

            if (UserInputRetriever.GetBool(string.Format("View {0}?", goodFilename)))
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