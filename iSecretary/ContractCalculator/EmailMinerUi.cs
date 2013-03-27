using System;
using System.Collections.Generic;
using ContractStatisticsAnalyser;
using Data;
using EmailDataMiner;

namespace UserInterface
{
    public class EmailMinerUi
    {
        public static void Run(Repository repo)
        {
            List<string> badResults;
            var goodResults = Extractor.GetEmailAddresses(repo.StorageWrapper.Data.EmailExportDirectory, out badResults, null);

            const string filename = "results.txt";
            var directory = repo.StorageWrapper.Data.EmailDataMiningResultsDirectory;
            var fullPath = directory + filename;

            Console.WriteLine("Saving...");
            DataWriter.WriteAll(directory, filename, goodResults, badResults);

            Console.WriteLine("A total of {0} email addresses saved to - {1}\n", goodResults.Count, fullPath);

            if (UserInputRetriever.GetBool(string.Format("View {0}?", filename)))
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