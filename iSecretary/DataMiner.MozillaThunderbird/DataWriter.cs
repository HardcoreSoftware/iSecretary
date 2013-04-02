using System.Collections.Generic;
using System.IO;
using System.Linq;
using IOInteraction;

namespace DataMiner.MozillaThunderbird
{
    public class DataWriter
    {
        public static void WriteAll(string directory, string goodFilename,string badFilename, string domainFilename, List<string> goodResults, List<string> badResults)
        {
            DirectoryCreator.EnsureExistance(directory);

            TextWriter tw = new StreamWriter(directory + goodFilename);
            foreach (var result in goodResults)
            {
                // write a line of text to the file
                tw.WriteLine(result);
            }
            tw.Close();


            tw = new StreamWriter(directory + domainFilename);
            var domains = goodResults.Where(x => x.Contains('@')).Select(x => x.Split('@')[1]).Distinct().ToList();
            domains.Sort();
            foreach (var result in domains)
            {
                tw.WriteLine(result);
            }
            tw.Close();


            tw = new StreamWriter(directory + badFilename);
            foreach (var result in badResults)
            {
                tw.WriteLine(result);
            }
            tw.Close();

        }
    }
}