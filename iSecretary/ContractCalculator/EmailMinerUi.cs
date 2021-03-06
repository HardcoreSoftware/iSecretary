using System;
using System.Collections.Generic;
using System.IO;
using ContractStatisticsAnalyser;
using Data;
using DataMiner.MozillaThunderbird;
using IOInteraction;

namespace UserInterface
{
    public class EmailMinerUi
    {
        public static void Run(Repository repo, string validEmailAddressesFilename, string invalidEmailAddressesFilename)
        {
            List<string> invalidemailAddresses;
            var validEmailAddresses = Extractor.GetEmailAddresses(repo.StorageWrapper.Data.MineableDataDirectory, out invalidemailAddresses, null);

            var directory = repo.StorageWrapper.Data.MineableDataResultsDirectory;
            var fullPath = directory + validEmailAddressesFilename;

            Console.WriteLine("Saving...");


            DirectoryCreator.EnsureExistance(directory + validEmailAddressesFilename);
            DirectoryCreator.EnsureExistance(directory + invalidEmailAddressesFilename);

            File.WriteAllLines(directory + validEmailAddressesFilename, validEmailAddresses);
            File.WriteAllLines(directory + invalidEmailAddressesFilename, invalidemailAddresses);

            Console.WriteLine("A total of {0} email addresses saved to - {1}\n", validEmailAddresses.Count, fullPath);

            if (UIRetriever.GetBool(string.Format("View {0}?", validEmailAddressesFilename)))
            {
                FileVisualiser.Show(fullPath);
            }

            if (UIRetriever.GetBool(string.Format("View {0}?", directory)))
            {
                DirectoryVisualiser.ShowFile(fullPath);
            }
        }
    }
}