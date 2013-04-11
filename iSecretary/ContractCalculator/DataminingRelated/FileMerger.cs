using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContractStatisticsAnalyser;
using Data;
using DataMiner.MozillaThunderbird;

namespace UserInterface.DataminingRelated
{
    public class FileMerger
    {
        public static void MergeFiles(Repository repo)
        {
            var f1 = repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.UniqueEmailAddressesFilename;
            var f2 = repo.StorageWrapper.Data.MineableDataDirectory + Extractor.LinkedInFilename;
            var f3 = repo.StorageWrapper.Data.MineableDataDirectory + Extractor.IgnoreListFilename;

            if (!File.Exists(f1))
            {
                Console.WriteLine("Unable to locate " + f1);
                return;
            }

            if (!File.Exists(f2))
            {
                Console.WriteLine("Unable to locate " + f2);
                return;
            }

            if (!File.Exists(f3))
            {
                Console.WriteLine("Unable to locate " + f3);
                return;
            }

            var mozilla = File.ReadLines(f1).ToList();
            var linkedIn = File.ReadLines(f2).ToList();
            var ignore = File.ReadLines(f3).ToList();
            var final = new List<string>();

            foreach (var line in mozilla.Where(line => !final.Contains(line)).Where(line => ignore.All(ignorePart => !line.Contains(ignorePart))))
            {
                final.Add(line);
            }
            foreach (var line in linkedIn.Where(line => !final.Contains(line)).Where(line => ignore.All(ignorePart => !line.Contains(ignorePart))))
            {
                final.Add(line);
            }

            final.Sort();

            File.WriteAllLines(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.ConvergedEmailAddressesFilename, final);

            var domains = final.Where(x => x.Contains('@')).Select(x => x.Split('@')[1]).Distinct().ToList();

            domains.Sort();

            File.WriteAllLines(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.UniqueDomainsFilename, domains);
            
            if (UIRetriever.GetBool(String.Format("View {0}?", Extractor.ConvergedEmailAddressesFilename)))
            {
                FileVisualiser.Show(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.ConvergedEmailAddressesFilename);
            }

            if (UIRetriever.GetBool(String.Format("View {0}?", repo.StorageWrapper.Data.MineableDataResultsDirectory)))
            {
                DirectoryVisualiser.ShowFile(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.ConvergedEmailAddressesFilename);
            }
        }
    }
}