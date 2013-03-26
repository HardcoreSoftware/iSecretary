using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using FileHelpers;
using IOInteraction;

namespace EmailDataMiner
{
    public class Extractor
    {
        public static void ParseAll(Repository repo)
        {
            var emailExportDirectory = repo.StorageWrapper.Data.EmailExportDirectory;

            DirectoryCreator.EnsureExistance(emailExportDirectory);

            var engine = new FileHelperEngine(typeof(MinableData));

            var results = new List<MinableData>();

            var directoryInfo = new DirectoryInfo(emailExportDirectory);

            if (!directoryInfo.GetFiles("*.csv").Any())
            {
                Console.WriteLine("No csv files found in " + emailExportDirectory);
                return;
            }

            foreach (var file in directoryInfo.GetFiles("*.csv"))
            {
                MinableData[] minableDatas;
                try
                {
                    minableDatas = engine.ReadFile(file.FullName) as MinableData[];

                }
                catch (IOException)
                {
                    Console.WriteLine("Please close any target CSV files.");
                    return;
                }
                if (minableDatas != null)
                {
                    results.AddRange(minableDatas.ToList());
                }
            }

            if (!results.Any())
            {
                Console.WriteLine("No data was imported.");
            }

            var i = results.Count;
        }
    }


    [DelimitedRecord(",")]
    public class MinableData
    {
        public string TitleSegment;
    }
}
