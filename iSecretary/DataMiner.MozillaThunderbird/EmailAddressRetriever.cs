using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMiner.MozillaThunderbird
{
    public class EmailAddressRetriever
    {
        public static void Get(ref List<string> results, ref List<string> badFiles, string[] filesToParse, int? cap)
        {
            var count = filesToParse.Count();
            int processed = 0;
            int percentIndicator = 0;

            foreach (var file in filesToParse)
            {
                if (processed == null || processed > cap)
                {
                    break;
                }

                bool isGood;
                var data = FromEmailAddressExtractor.Retrieve(file, out isGood);

                if (isGood)
                {
                    results.Add(data);
                }
                else
                {
                    badFiles.Add(data);
                }


                var percent = (int)Math.Round((100.0 / (cap ?? count) ) * processed);
                if (percent != percentIndicator)
                {
                    percentIndicator = percent;
                    Console.WriteLine("{0}% complete", percentIndicator);
                }

                processed++;
            }
        }
    }
}