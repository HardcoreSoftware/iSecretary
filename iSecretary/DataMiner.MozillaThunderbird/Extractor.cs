﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMiner.MozillaThunderbird
{
    public class Extractor
    {
        const string Ext = "*.html";

        public static List<string> GetEmailAddresses(string emailExportDirectory, out List<string> badFiles, int? cap)
        {
            var files = FileRetriever.GetFiles(emailExportDirectory, Ext);

            badFiles = new List<string>();

            if (!files.Any())
            {
                Console.WriteLine("No {0} files found in {1}", Ext, emailExportDirectory);
                return null;
            }

            var filesToParse = files.Where(x => !x.ToLower().EndsWith("index.html"));

            var results = new List<string>();

            EmailAddressRetriever.Get(ref results, ref badFiles, filesToParse as string[] ?? filesToParse.ToArray(),cap);

            if (!results.Any())
            {
                Console.WriteLine("No data was imported.");
            }

            
            List<string> postProcessFailures;

            results = DataPostProcesser.ProcessRawMatches(results, out postProcessFailures);
            badFiles.AddRange(postProcessFailures);

            Console.WriteLine("Removing duplicates...");
            var list = results.Distinct().ToList();
            
            Console.WriteLine("Sorting...");
            list.Sort();

            return list;
        }
    }
}