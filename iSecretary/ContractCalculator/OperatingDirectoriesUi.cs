using System;
using System.IO;
using ContractStatisticsAnalyser;
using Data;
using Data.Entities;
using Extensions;

namespace UserInterface
{
    public class OperatingDirectoriesUi
    {
        public static void SetStorageDirectories(Repository repo)
        {
            var invoiceDirectory = GetInvoiceDirectory(Nameof<StorageEntity>.Property(e => e.InvoiceDirectory));
            var emailExportDirectory = GetInvoiceDirectory(Nameof<StorageEntity>.Property(e => e.MineableDataDirectory));
            var emailDataMiningResultsDirectory = GetInvoiceDirectory(Nameof<StorageEntity>.Property(e => e.MineableDataResultsDirectory));

            repo.StorageWrapper.Data.InvoiceDirectory = invoiceDirectory;
            repo.StorageWrapper.Data.MineableDataDirectory = emailExportDirectory;
            repo.StorageWrapper.Data.MineableDataResultsDirectory = emailDataMiningResultsDirectory;
            repo.StorageWrapper.Save();
        }

        private static string GetInvoiceDirectory(string directoryDescription)
        {
            var invoiceDirectory = PromptForDirectory(directoryDescription);
            var tryAgain = true;
            while (tryAgain)
            {
                if (Directory.Exists(invoiceDirectory))
                {
                    tryAgain = false;
                    Console.WriteLine("\nInvoice directory set to \"{0}\".\n", directoryDescription);
                }
                else
                {
                    Console.WriteLine("Invalid directory. Please try agian.");
                    invoiceDirectory = PromptForDirectory(directoryDescription);
                }
            }
            return invoiceDirectory;
        }

        private static string PromptForDirectory(string directoryDescription)
        {
            return UserInputRetriever.GetString(string.Format("Select directory for '{0}'", directoryDescription));
        }

        public static void ViewInvoiceDirectory(Repository repo)
        {
            DirectoryVisualiser.ShowDirectory(repo.StorageWrapper.Data.InvoiceDirectory);
        }

        public static void ViewEmailExports(Repository repo)
        {
            DirectoryVisualiser.ShowDirectory(repo.StorageWrapper.Data.MineableDataDirectory);
        }
    }
}